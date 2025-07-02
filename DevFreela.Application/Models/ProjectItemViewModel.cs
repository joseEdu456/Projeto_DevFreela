using DevFreela.Core.Entities;

namespace DevFreela.Application.Models
{
    public class ProjectItemViewModel
    {

        public int Id { get; private set; }
        public string Titulo { get; private set; }
        public string ClienteNome { get; private set; }
        public string FreelancerNome { get; private set; }
        public decimal CustoTotal { get; private set; }
        public ProjectItemViewModel(int id, string titulo, string clienteNome, string freelancerNome, decimal custoTotal)
        {
            Id = id;
            Titulo = titulo;
            ClienteNome = clienteNome;
            FreelancerNome = freelancerNome;
            CustoTotal = custoTotal;
        }

        public static ProjectItemViewModel ToEntity(Project project)
        {
            return new ProjectItemViewModel(project.Id, project.Title, project.Cliente.Nome, project.Freelancer.Nome, project.CustoTotal);
        }
        
    }
}
