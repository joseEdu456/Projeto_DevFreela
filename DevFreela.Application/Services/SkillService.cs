using DevFreela.Application.Interfaces;
using DevFreela.Application.Models;
using DevFreela.Core.Entities;
using DevFreela.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DevFreela.Application.Services
{
    public class SkillService : ISkillService
    {
        private readonly DevFreelaDbContext _context;

        public SkillService(DevFreelaDbContext context)
        {
            _context = context;
        }

        public ResultViewModel<List<Habilidade>> GetAll()
        {
            var skills = _context.Habilidades.ToList();

            if (!skills.Any())
            {
                return ResultViewModel<List<Habilidade>>.Error("Nehuma habilidade cadastrada");
            }

            return ResultViewModel<List<Habilidade>>.Sucess(skills);
        }

        public ResultViewModel InsereSkill(CreateSkillInputModel Skillmodel)
        {
            var skill = new Habilidade(Skillmodel.Descricao);

            _context.Habilidades.Add(skill);
            _context.SaveChanges();

            return ResultViewModel.Sucess();
        }
    }
}
