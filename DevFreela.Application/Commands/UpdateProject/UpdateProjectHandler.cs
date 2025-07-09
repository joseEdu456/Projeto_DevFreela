using DevFreela.Application.Models;
using DevFreela.Infrastructure.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DevFreela.Application.Commands.UpdateProject
{
    public class UpdateProjectHandler : IRequestHandler<UpdateProjectCommand, ResultViewModel>
    {
        private readonly DevFreelaDbContext _db;
        public UpdateProjectHandler(DevFreelaDbContext db)
        {
            _db = db;
        }

        public async Task<ResultViewModel> Handle(UpdateProjectCommand request, CancellationToken cancellationToken)
        {
            var projeto = await _db.Projects.SingleOrDefaultAsync(p => p.Id == request.IdProject);

            if (projeto is null)
            {
                return ResultViewModel.Error("Projeto não encontrado");
            }

            projeto.AtualizarDados(request.Title, request.Descricao, request.TotalCost);

            _db.Projects.Update(projeto);
            await _db.SaveChangesAsync();

            return ResultViewModel.Sucess();
        }
    }
}
