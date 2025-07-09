using DevFreela.Application.Models;
using DevFreela.Infrastructure.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace DevFreela.Application.Queries.GetProjectById
{
    public class GetProjectByIdHandler : IRequestHandler<GetProjectByIdQuery, ResultViewModel<ProjectViewModel>>
    {
        private readonly DevFreelaDbContext _db;
        public GetProjectByIdHandler(DevFreelaDbContext db)
        {
            _db = db;
        }

        public async Task<ResultViewModel<ProjectViewModel>> Handle(GetProjectByIdQuery request, CancellationToken cancellationToken)
        {
            var projeto = await _db.Projects
                .Include(p => p.Cliente)
                .Include(p => p.Freelancer)
                .Include(p => p.Comentarios)
                .FirstOrDefaultAsync(p => p.Id == request.Id);

            if (projeto == null)
                return ResultViewModel<ProjectViewModel>.Error("Projeto não encontrado");

            var model = ProjectViewModel.FromEntidade(projeto);

            return ResultViewModel<ProjectViewModel>.Sucess(model);
        }
    }
}
