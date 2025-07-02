using DevFreela.Core.Entities;

namespace DevFreela.Application.Models
{
    public class UserViewModel
    {
        public UserViewModel(string nome, string email, DateTime dtNascimento, List<string> habilidades)
        {
            Nome = nome;
            Email = email;
            DtNascimento = dtNascimento;
            Habilidades = habilidades;
        }

        public string Nome { get; private set; }
        public string Email { get; private set; }
        public DateTime DtNascimento { get; private set; }
        public List<string> Habilidades { get; private set; }

        public static UserViewModel FromEntity(User user)
        {
            var habilidades = user.Habilidades.Select(h => h.Habilidade.Descricao).ToList();
            return new UserViewModel(user.Nome, user.Email, user.DtNascimento, habilidades);
        } 
    }
}
