namespace DevFreela.Core.Entities
{
    public class User : BaseEntity
    {
        public string Nome { get; private set; }
        public string Email { get; private set; }
        public DateTime DtNascimento { get; private set; }
        public bool Ativo { get; private set; }
        public string Password { get; private set; }
        public string Role { get; private set; }
        public List<UserHabilidade> Habilidades { get; private set; }
        public List<Comentario> Comentarios { get; private set; }
        public List<Project> MeusProjetos { get; private set; }
        public List<Project> FreelanceProjetos { get; private set; }

        public User(string nome, string email, DateTime dtNascimento, string password, string role) : base()
        {
            Nome = nome;
            Email = email;
            DtNascimento = dtNascimento;
            Ativo = true;
            Password = password;
            Role = role;

            Habilidades = [];
            Comentarios = [];
            MeusProjetos = [];
            FreelanceProjetos = [];
        }

    }
}
