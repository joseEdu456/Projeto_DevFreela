using DevFreela.Application.Models;
using DevFreela.Infrastructure.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace DevFreela.Application.Queries.GetAllProjects
{
    public class GetAllProjectHandler : IRequestHandler<GetAllProjectQuery, ResultViewModel<List<ProjectItemViewModel>>>
    {
        private readonly DevFreelaDbContext _db;
        public GetAllProjectHandler(DevFreelaDbContext db)
        {
            _db = db;
        }

        public async Task<ResultViewModel<List<ProjectItemViewModel>>> Handle(GetAllProjectQuery request, CancellationToken cancellationToken)
        {
            var projetos = await _db.Projects
                .Include(p => p.Cliente)
                .Include(p => p.Freelancer)
                .Where(p => !p.IsDeleted && (request.Search == "" || p.Title.Contains(request.Search) || p.Description.Contains(request.Search)))
                .Skip(request.Page * request.Tamanho)
                .Take(request.Tamanho)
                .ToListAsync();

            if (!projetos.Any())
            {
                return ResultViewModel<List<ProjectItemViewModel>>.Error("Nenhum projeto encontrado");
            }

            var model = projetos.Select(ProjectItemViewModel.ToEntity).ToList();

            return ResultViewModel<List<ProjectItemViewModel>>.Sucess(model);
        }
    }
}
