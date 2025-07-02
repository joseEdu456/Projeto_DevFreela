using DevFreela.Core.Entities;

namespace DevFreela.Application.Models
{
    public class ProjectViewModel
    {
        public int Id { get; private set; }
        public string Titulo { get; private set; }
        public string Descricao { get; private set; }
        public int IdCliente { get; private set; }
        public int IdFreelancer { get; private set; }
        public string ClienteNome { get; private set; }
        public string FreelancerNome { get; private set; }
        public decimal CustoTotal { get; private set; }
        public List<string> Comentarios { get; private set; }
        public ProjectViewModel(int id, string titulo, string descricao, int idCliente, int idFreelancer, string clienteNome, string freelancerNome, decimal custoTotal, List<Comentario> comentarios)
        {
            Id = id;
            Titulo = titulo;
            Descricao = descricao;
            IdCliente = idCliente;
            IdFreelancer = idFreelancer;
            ClienteNome = clienteNome;
            FreelancerNome = freelancerNome;
            CustoTotal = custoTotal;
            Comentarios = comentarios.Select(c => c.Conteudo).ToList();
        }

        public static ProjectViewModel FromEntidade(Project entidade)
        {
            return new ProjectViewModel(
                entidade.Id, entidade.Title, entidade.Description, entidade.IdCliente,
                entidade.IdFreelancer, entidade.Cliente.Nome, entidade.Freelancer.Nome,
                entidade.CustoTotal, entidade.Comentarios
            );
        }
    }
}
