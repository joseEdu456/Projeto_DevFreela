using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Runtime.Intrinsics.X86;
using System.Text;
using System.Threading.Tasks;
using DevFreela.Application.Commands.DeleteProject;
using DevFreela.Core.Entities;
using DevFreela.Core.Repositories;
using Microsoft.Identity.Client;
using NSubstitute;

namespace DevFreela.UnitTests.Application
{
    public class DeleteProjectHandlerTests
    {
        [Fact]
        public async Task ProjectExists_Delete_Sucess_NSubstitute()
        {
            // Arrange

            //Configurando projeto exemplo
            var project = new Project("Project Teste", "Teste do project", 1, 2, 1000);

            var repository = Substitute.For<IProjectRepository>(); // Fazendo o mock do repository
            repository.GetById(1).Returns(Task.FromResult((Project?)project)); // Configurando método GetById para retornar o projeto exemplo
            repository.Delete(Arg.Any<Project>()).Returns(Task.CompletedTask); // Configurando método Update para completar a tarefa sem erro

            var handler = new DeleteProjectHandler(repository);

            var command = new DeleteProjectCommand(1);
            // Act
            var result = await handler.Handle(command, new CancellationToken());

            // Assert
            Assert.True(result.IsSucess); // Verificando se o resultado deu sucesso
            await repository.Received(1).GetById(Arg.Any<int>());  // Verificando se o método GetById foi chamado uma vez com qualquer valor
            await repository.Received(1).Delete(Arg.Any<Project>()); // Verificando se o método Update foi chamado uma vez com qualquer project
        }

        [Fact]
        public async Task ProjectDoesNotExists_Delete_Error_NSubstitute()
        {
            const string messageError = "Projeto não encontrado"; // Mensagem de erro esperada

            // Arrange
            var repository = Substitute.For<IProjectRepository>(); // mock do repository
            repository.GetById(Arg.Any<int>()).Returns(Task.FromResult((Project?)null)); // Configurando método GetById para retornar null, simulando que o projeto não existe

            var handler = new DeleteProjectHandler(repository);

            var command = new DeleteProjectCommand(1);
            // Act
            var result = await handler.Handle(command, new CancellationToken());

            // Assert
            Assert.False(result.IsSucess); // Verificando se o resultado não deu sucesso
            Assert.Equal(messageError, result.Message); // Verificando se a mensagem de erro é a esperada
            await repository.Received(1).GetById(Arg.Any<int>()); // Verificando se o método GetById foi chamado uma vez com qualquer valor
            await repository.DidNotReceive().Delete(Arg.Any<Project>()); // Verificando se o método Delete não foi chamado, pois o projeto não existe
        }
    }
}
