using DevFreela.Core.Entities;
using DevFreela.Application.Models;
using DevFreela.Infrastructure.Persistence;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using DevFreela.Application.Interfaces;

namespace DevFreela.API.Controllers
{
    [Route("api/users")]
    [ApiController]
    public class UsersControllers : ControllerBase
    {
        private readonly DevFreelaDbContext _db;
        private readonly IUserService _service;

        public UsersControllers(DevFreelaDbContext db, IUserService service)
        {
            _db = db;
            _service = service;
        }

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var result = _service.GetById(id);

            if (!result.IsSucess)
            {
                return BadRequest(result.Message);
            }

            return Ok(result);
        }

        // POST api/users
        [HttpPost]
        public IActionResult Post(CreatedUserInputModel userModel)
        {
            var user = _service.InserirUser(userModel);

            return CreatedAtAction(nameof(GetById), new {id = user.Data}, userModel);
        }

        [HttpPost("{id}/skills")]
        public IActionResult PostSkills(int id, UserSkillsInputModel model)
        {
            var result = _service.InserirSkillUser(id, model);

            return NoContent();
        }

    }
}
