using DevFreela.Application.Models;
using DevFreela.Core.Entities;
using DevFreela.Core.Repositories;
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
        private readonly IProjectRepository _repository;
        public InsertCommentHandler(IProjectRepository repository)
        {
            _repository = repository;
        }

        public async Task<ResultViewModel> Handle(InsertCommentCommand request, CancellationToken cancellationToken)
        {
            var projetoExist = await _repository.Exists(request.IdProject);

            if (!projetoExist)
            {
                return ResultViewModel.Error("Projeto não encontrado");
            }

            var comentario = new Comentario(request.Text, request.IdProject, request.IdUser);

            await _repository.AddComment(comentario);

            return ResultViewModel.Sucess();
        }
    }
}
