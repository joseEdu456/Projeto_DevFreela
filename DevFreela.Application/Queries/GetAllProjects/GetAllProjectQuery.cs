using Azure;
using DevFreela.Application.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DevFreela.Application.Queries.GetAllProjects
{
    public class GetAllProjectQuery : IRequest<ResultViewModel<List<ProjectItemViewModel>>>
    {

        public string Search { get; set; }
        public int Page { get; set; }
        public int Tamanho { get; set; }
        public GetAllProjectQuery(string search, int page, int tamanho)
        {
            Search = search;
            Page = page;
            Tamanho = tamanho;
        }
    }
}
