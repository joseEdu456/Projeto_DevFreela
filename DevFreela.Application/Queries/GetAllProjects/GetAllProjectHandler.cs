using DevFreela.Application.Models;
using DevFreela.Core.Repositories;
using DevFreela.Infrastructure.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace DevFreela.Application.Queries.GetAllProjects
{
    public class GetAllProjectHandler : IRequestHandler<GetAllProjectQuery, ResultViewModel<List<ProjectItemViewModel>>>
    {
        private readonly IProjectRepository _repository;
        public GetAllProjectHandler(IProjectRepository repository)
        {
            _repository = repository;
        }

        public async Task<ResultViewModel<List<ProjectItemViewModel>>> Handle(GetAllProjectQuery request, CancellationToken cancellationToken)
        {
            var projetos = await _repository.GetAllFiltro(request.Search, request.Page, request.Tamanho);

            if (!projetos.Any())
            {
                return ResultViewModel<List<ProjectItemViewModel>>.Error("Nenhum projeto encontrado");
            }

            var model = projetos.Select(ProjectItemViewModel.ToEntity).ToList();

            return ResultViewModel<List<ProjectItemViewModel>>.Sucess(model);
        }
    }
}
