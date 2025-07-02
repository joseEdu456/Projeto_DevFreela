using DevFreela.Application.Models;
using DevFreela.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DevFreela.Application.Interfaces
{
    public interface ISkillService
    {
        public ResultViewModel<List<Habilidade>> GetAll();
        public ResultViewModel InsereSkill(CreateSkillInputModel Skillmodel);
    }
}
