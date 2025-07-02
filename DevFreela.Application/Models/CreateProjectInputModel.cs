using DevFreela.Core.Entities;

namespace DevFreela.Application.Models
{
    public class CreateProjectInputModel
    {
        public string Title { get; set; }
        public string Descricao { get; set; }
        public int IdClient { get; set; }
        public int IdFreelancer { get; set; }
        public decimal TotalCost { get; set; }

        public Project ToEntity()
        {
            return new Project(Title, Descricao, IdClient, IdFreelancer, TotalCost);
        }
    }
}
