namespace DevFreela.Core.Entities
{
    public class Comentario : BaseEntity
    {
        public string Conteudo { get; private set; }
        public int IdProjeto { get; private set; }
        public Project Projeto { get; private set; }
        public int IdUser { get; private set; }
        public User User { get; private set; }

        public Comentario(string conteudo, int idProjeto, int idUser) : base()
        {
            Conteudo = conteudo;
            IdProjeto = idProjeto;
            IdUser = idUser;
        }
    }
}
