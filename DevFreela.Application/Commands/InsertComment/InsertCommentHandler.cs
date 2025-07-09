using DevFreela.Application.Models;
using DevFreela.Core.Entities;
using DevFreela.Infrastructure.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DevFreela.Application.Commands.InsertComment
{
    public class InsertCommentHandler : IRequestHandler<InsertCommentCommand, ResultViewModel>
    {
        private readonly DevFreelaDbContext _db;
        public InsertCommentHandler(DevFreelaDbContext db)
        {
            _db = db;
        }

        public async Task<ResultViewModel> Handle(InsertCommentCommand request, CancellationToken cancellationToken)
        {
            var projeto = await _db.Projects.SingleOrDefaultAsync(p => p.Id == request.IdProject);

            if (projeto is null)
            {
                return ResultViewModel.Error("Projeto não encontrado");
            }

            var comentario = new Comentario(request.Text, request.IdProject, request.IdUser);

            _db.Comentarios.Add(comentario);
            await _db.SaveChangesAsync();

            return ResultViewModel.Sucess();
        }
    }
}
