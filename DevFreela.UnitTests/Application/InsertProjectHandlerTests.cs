using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DevFreela.Application.Commands.InsertProject;
using DevFreela.Core.Entities;
using DevFreela.Core.Repositories;
using NSubstitute;

namespace DevFreela.UnitTests.Application
{
    public class InsertProjectHandlerTests
    {
        [Fact]
        public async Task InputDataAreOk_Insert_Sucess_NSubstitute()
        {
            const int ID = 1;

            //Arrange
            var repository = Substitute.For<IProjectRepository>(); // Fazendo o mock do repository
            repository.AddProject(Arg.Any<Project>()).Returns(Task.FromResult(ID));
            // Configurando que o retorno do AddProject() de qualquer Project deverá ser ID (neste caso valor 1)

            var command = new InsertProjectCommand
            {
                Title = "Project Teste",
                Descricao = "Teste do project",
                IdClient = 1,
                IdFreelancer = 2,
                TotalCost = 1000
            };

            var handler = new InsertProjectHandler(repository);
            // Instanciando o handler passando o repository mockado

            //Act
            var result = await handler.Handle(command, new CancellationToken());

            //Assert
            Assert.True(result.IsSucess); // Verifica se o resultado é sucesso
            Assert.Equal(ID, result.Data); // Verifica se o ID retornado é igual ao esperado
            
            await repository.Received(1).AddProject(Arg.Any<Project>());
            // Verifica se o método AddProject foi chamado uma vez com qualquer instância de Project
        }
    }
}
