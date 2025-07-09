using DevFreela.Application.Models;
using DevFreela.Infrastructure.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DevFreela.Application.Commands.CompleteProject
{
    public class CompleteProjectHandler : IRequestHandler<CompleteProjectCommand, ResultViewModel>
    {
        private readonly DevFreelaDbContext _db;
        public CompleteProjectHandler(DevFreelaDbContext db)
        {
            _db = db;
        }

        public async Task<ResultViewModel> Handle(CompleteProjectCommand request, CancellationToken cancellationToken)
        {
            var projeto = await _db.Projects.SingleOrDefaultAsync(p => p.Id == request.Id);

            if (projeto is null)
            {
                return ResultViewModel.Error("Projeto não encontrado");
            }

            projeto.Finalizar();

            _db.Projects.Update(projeto);
            await _db.SaveChangesAsync();

            return ResultViewModel.Sucess();
        }
    }
}
