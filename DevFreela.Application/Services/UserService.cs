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
    public class UserService : IUserService
    {
        private readonly DevFreelaDbContext _db;
        public UserService(DevFreelaDbContext db)
        {
            _db = db;
        }

        public ResultViewModel<UserViewModel> GetById(int id)
        {
            var user = _db.Users
                .Include(u => u.Habilidades)
                .ThenInclude(u => u.Habilidade)
                .SingleOrDefault(u => u.Id == id);

            if (user.IsDeleted || user is null)
            {
                return ResultViewModel<UserViewModel>.Error("Usuário não encontrado");
            }

            var model = UserViewModel.FromEntity(user);

            return ResultViewModel<UserViewModel>.Sucess(model);
        }

        public ResultViewModel InserirSkillUser(int idUser, UserSkillsInputModel model)
        {
            var userSkill = model.SkillIds.Select(s => new UserHabilidade(idUser, s)).ToList();

            _db.UserHabilidades.AddRange(userSkill);
            _db.SaveChanges();

            return ResultViewModel.Sucess();
        }

        public ResultViewModel InserirUser(CreatedUserInputModel model)
        {
            var user = new User(model.Nome, model.Email, model.DtNascimento);

            _db.Users.Add(user);
            _db.SaveChanges();

            return ResultViewModel.Sucess();
        }
    }
}
