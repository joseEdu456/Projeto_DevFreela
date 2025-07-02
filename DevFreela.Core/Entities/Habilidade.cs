namespace DevFreela.Core.Entities
{
    public class Habilidade : BaseEntity
    {
        public Habilidade(string descricao) : base()
        {
            Descricao = descricao;
        }

        public string Descricao { get; private set; }
        public List<UserHabilidade> UserHabilidades { get; private set; }
    }
}
