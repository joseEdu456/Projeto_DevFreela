using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DevFreela.Core.Entities;
using DevFreela.Core.Enums;

namespace DevFreela.UnitTests.Core
{
    public class ProjectTests
    {
        [Fact]
        public void ProjectIsCreated_Start_Sucess()
        {
            //Arrange
            var project = new Project("Project Teste", "Teste do project", 1, 2, 1000); 

            //Act
            project.Iniciar();

            //Assert
            Assert.Equal(EnumProjectStatus.EmProgresso, project.Status);
            Assert.NotNull(project.DataInicioProjeto);

            Assert.True(project.Status == EnumProjectStatus.EmProgresso);
            Assert.False(project.DataInicioProjeto is null);
        }

        [Fact]
        public void ProjectIsInInvalidState_Start_ThrowExecption()
        {
            //Arrange 
            var project = new Project("Project Teste", "Teste do project", 1, 2, 1000);
            project.Iniciar(); // Inicia o projeto para mudar o status

            //Act + Assert
            Action? start = project.Iniciar;

            //Verfica se a action gera a exception que foi passada no <>, neste caso InvalidOperationException
            var execption = Assert.Throws<InvalidOperationException>(start);

            //Verifica se a mensagem da exceção é a esperada com a que foi gerada
            Assert.Equal(Project.INVALID_OPERATION_MESSAGE, execption.Message);
        }
    }
}
