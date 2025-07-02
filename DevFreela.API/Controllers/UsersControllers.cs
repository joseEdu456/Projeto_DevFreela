using DevFreela.Core.Entities;
using DevFreela.Application.Models;
using DevFreela.Infrastructure.Persistence;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DevFreela.API.Controllers
{
    [Route("api/users")]
    [ApiController]
    public class UsersControllers : ControllerBase
    {
        private readonly DevFreelaDbContext _db;

        public UsersControllers(DevFreelaDbContext db)
        {
            _db = db;
        }

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var user = _db.Users
                .Include(u => u.Habilidades)
                .ThenInclude(u => u.Habilidade)
                .SingleOrDefault(u => u.Id == id);

            if(user.IsDeleted || user is null)
            {
                return NotFound();
            }

            var model = UserViewModel.FromEntity(user);

            return Ok(model);
        }

        // POST api/users
        [HttpPost]
        public IActionResult Post(CreatedUserInputModel userModel)
        {
            var user = new User(userModel.Nome, userModel.Email, userModel.DtNascimento);

            _db.Users.Add(user);
            _db.SaveChanges();

            return Ok();
        }

        [HttpPost("{id}/skills")]
        public IActionResult PostSkills(int id, UserSkillsInputModel model)
        {
            var userSkill = model.SkillIds.Select(s => new UserHabilidade(id, s)).ToList();

            _db.UserHabilidades.AddRange(userSkill);
            _db.SaveChanges();

            return NoContent();
        }

    }
}
