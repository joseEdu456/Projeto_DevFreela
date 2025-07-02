using DevFreela.Application.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DevFreela.Application.Interfaces
{
    public interface IUserService
    {
        public ResultViewModel<UserViewModel> GetById(int id);
        public ResultViewModel InserirUser(CreatedUserInputModel model);
        public ResultViewModel InserirSkillUser(int idUser, UserSkillsInputModel model);
    }
}
