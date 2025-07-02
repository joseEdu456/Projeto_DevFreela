namespace DevFreela.Application.Models
{
    public class CreateProjectCommentInputModel
    {
        public string Text { get; set; }
        public int IdProject { get; set; }
        public int IdUser { get; set; }
    }
}
