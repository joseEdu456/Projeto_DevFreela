﻿namespace DevFreela.Application.Models
{
    public class UpdateProjectInputModel
    {
        public int IdProject { get; set; }
        public string Title { get; set; }
        public string Descricao { get; set; }
        public decimal TotalCost { get; set; }
    }
}
