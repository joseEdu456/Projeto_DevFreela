using DevFreela.Application.Models;
using DevFreela.Infrastructure.Persistence;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DevFreela.Application.Commands.InsertProject
{
    public class InsertProjectHandler : IRequestHandler<InsertProjectCommand, ResultViewModel<int>>
    {
        private readonly DevFreelaDbContext _db;
        public InsertProjectHandler(DevFreelaDbContext db)
        {
            _db = db;
        }

        public async Task<ResultViewModel<int>> Handle(InsertProjectCommand request, CancellationToken cancellationToken)
        {
            var projeto = request.ToEntity();

            await _db.Projects.AddAsync(projeto);
            await _db.SaveChangesAsync();

            return ResultViewModel<int>.Sucess(projeto.Id);
        }
    }
}
