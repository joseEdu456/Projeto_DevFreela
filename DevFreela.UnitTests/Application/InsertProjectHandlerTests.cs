using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DevFreela.Application.Commands.InsertProject;
using DevFreela.Core.Entities;
using DevFreela.Core.Repositories;
using DevFreela.UnitTests.Faker;
using FluentAssertions;
using Moq;
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

            //var command = new InsertProjectCommand
            //{
            //    Title = "Project Teste",
            //    Descricao = "Teste do project",
            //    IdClient = 1,
            //    IdFreelancer = 2,
            //    TotalCost = 1000
            //};

            var command = FakeDataHelper.CreateFakeInsertProjectCommand();

            var handler = new InsertProjectHandler(repository);
            // Instanciando o handler passando o repository mockado

            //Act
            var result = await handler.Handle(command, new CancellationToken());

            //Assert

            // Usando o fluent assertion para escrever validaçõoes de forma mais clara
            result.IsSucess.Should().BeTrue();

            result.Data.Should().Be(ID); // Verifica se o ID retornado é igual ao esperado
            
            await repository.Received(1).AddProject(Arg.Any<Project>());
            // Verifica se o método AddProject foi chamado uma vez com qualquer instância de Project
        }

        [Fact]
        public async Task InputDataAreOk_Insert_Sucess_Moq()
        {
            const int ID = 1;

            //Arrange

            var mock = new Mock<IProjectRepository>(); // Objeto do tipo Mock
            mock.Setup(r => r.AddProject(It.IsAny<Project>())).ReturnsAsync(ID); // Configuração do mock

            //Simplificação do código acima
            var repository = Mock
                .Of<IProjectRepository>(r => r.AddProject(It.IsAny<Project>()) == Task.FromResult(ID));
            // Fazendo configuração e criando o repository em um código apenas

            //var command = new InsertProjectCommand
            //{
            //    Title = "Project Teste",
            //    Descricao = "Teste do project",
            //    IdClient = 1,
            //    IdFreelancer = 2,
            //    TotalCost = 1000
            //};

            var command = FakeDataHelper.CreateFakeInsertProjectCommand();

            var handler = new InsertProjectHandler(repository);
            // Instanciando o handler passando o repository mockado

            //Act
            var result = await handler.Handle(command, new CancellationToken());

            //Assert
            Assert.True(result.IsSucess); // Verifica se o resultado é sucesso
            Assert.Equal(ID, result.Data); // Verifica se o ID retornado é igual ao esperado

            //mock.Verify(m => m.AddProject(It.IsAny<Project>()), Times.Once); 
            Mock.Get(repository).Verify(m => m.AddProject(It.IsAny<Project>()), Times.Once); // Verifica se o método de adicionar qualquer projeto é chamado apenas uma vez
        }
    }
}
