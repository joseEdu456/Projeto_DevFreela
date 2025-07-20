using DevFreela.Application.Models;
using DevFreela.Core.Repositories;
using DevFreela.Infrastructure.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DevFreela.Application.Commands.StartProject
{
    public class StartProjectHandler : IRequestHandler<StartProjectCommand, ResultViewModel>
    {
        private readonly IProjectRepository _repository;
        public StartProjectHandler(IProjectRepository repository)
        {
            _repository = repository;
        }

        public async Task<ResultViewModel> Handle(StartProjectCommand request, CancellationToken cancellationToken)
        {
            var projeto = await _repository.GetById(request.Id);

            if (projeto is null)
            {
                return ResultViewModel.Error("Projeto não encontrado");
            }

            projeto.Iniciar();

            await _repository.Start(projeto);

            return ResultViewModel.Sucess();
        }
    }
}
