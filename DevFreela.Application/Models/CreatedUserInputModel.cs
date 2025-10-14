namespace DevFreela.Application.Models
{
    public class CreatedUserInputModel
    {
        public string Nome { get;  set; }
        public string Email { get; set; }
        public DateTime DtNascimento { get; set; }
        public string Senha { get; set; }
        public string Role { get; set; }
    }
}
