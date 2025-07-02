namespace DevFreela.Core.Entities
{
    public class UserHabilidade : BaseEntity
    {
        public int IdUser { get; private set; }
        public User User { get; private set; }
        public int IdHabilidade { get; private set; }
        public Habilidade Habilidade { get; private set; }

        public UserHabilidade(int idUser, int idHabilidade) : base()
        {
            IdUser = idUser;
            IdHabilidade = idHabilidade;
        }
    }
}
