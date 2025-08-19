using DevFreela.Core.Enums;
using System.Reflection.Metadata.Ecma335;

namespace DevFreela.Core.Entities
{
    public class Project : BaseEntity
    {
        public const string INVALID_OPERATION_MESSAGE = "Projeto não está mais no status de Criado";
        public string Title { get; private set; }
        public string Description { get; private set; }
        public int IdCliente { get; private set; }
        public User Cliente { get; private set; }
        public int IdFreelancer { get; private set; }
        public User Freelancer { get; private set; }
        public decimal CustoTotal { get; private set; }
        public DateTime? DataInicioProjeto { get; private set; }
        public DateTime? DataFimProjeto { get; private set; }
        public EnumProjectStatus Status { get; private set; }
        public List<Comentario> Comentarios { get; private set; }

        protected Project() { }
        public Project(string title, string description, int idCliente, int idFreelancer, decimal custoTotal) : base()
        {
            Title = title;
            Description = description;
            IdCliente = idCliente;
            IdFreelancer = idFreelancer;
            CustoTotal = custoTotal;

            Status = EnumProjectStatus.Criado;
            Comentarios = [];
        }

        public void Cancelar()
        {
            if(Status == EnumProjectStatus.EmProgresso || Status == EnumProjectStatus.Suspenso)
            {
                Status = EnumProjectStatus.Cancelado;
                DataFimProjeto = DateTime.Now;
            }
        }

        public void Iniciar()
        {
            if (Status != EnumProjectStatus.Criado )
            {
                throw new InvalidOperationException(INVALID_OPERATION_MESSAGE);
            }

            Status = EnumProjectStatus.EmProgresso;
            DataInicioProjeto = DateTime.Now;
        }
        public void Finalizar()
        {
            if (Status == EnumProjectStatus.EmProgresso || Status == EnumProjectStatus.PagamentoPendente)
            {
                Status = EnumProjectStatus.Finalizado;
                DataFimProjeto = DateTime.Now;
            }
        }

        public void SetarPagamentoPendente()
        {
            if (Status == EnumProjectStatus.EmProgresso)
            {
                Status = EnumProjectStatus.PagamentoPendente;
            }
        }

        public void AtualizarDados(string titulo, string descricao, decimal custo)
        {
            Title = titulo;
            Description = descricao;
            CustoTotal = custo;
        }

    }
}
