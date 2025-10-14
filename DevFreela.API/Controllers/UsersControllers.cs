using DevFreela.Core.Entities;
using DevFreela.Application.Models;
using DevFreela.Infrastructure.Persistence;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using DevFreela.Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
using DevFreela.Infrastructure.Auth;

namespace DevFreela.API.Controllers
{
    [Route("api/users")]
    [ApiController]
    [Authorize]
    public class UsersControllers : ControllerBase
    {
        private readonly DevFreelaDbContext _db;
        private readonly IUserService _service;
        private readonly IAuthService _auth;

        public UsersControllers(DevFreelaDbContext db, IUserService service, IAuthService auth)
        {
            _db = db;
            _service = service;
            _auth = auth;
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

        [HttpPost("{id}/skills")]
        public IActionResult PostSkills(int id, UserSkillsInputModel model)
        {
            var result = _service.InserirSkillUser(id, model);

            return NoContent();
        }

        // POST api/users
        [HttpPost]
        [AllowAnonymous]
        public IActionResult Post(CreatedUserInputModel userModel)
        {
            var hashSenha = _auth.ComputeHash(userModel.Senha);
            userModel.Senha = hashSenha;

            var user = _service.InserirUser(userModel);

            return CreatedAtAction(nameof(GetById), new {id = user.Data}, userModel);
        }

        [HttpPut("login")]
        [AllowAnonymous]
        public IActionResult Login(LoginInputModel model)
        {
            var hash = _auth.ComputeHash(model.Password);

            var user = _db.Users.SingleOrDefault(u => u.Email == model.Email && u.Password == hash);

            if(user is null)
            {
                var error = ResultViewModel<LoginViewModel>.Error("Erro de login");

                return BadRequest(error);
            }

            var token = _auth.GenerateToken(user.Email, user.Role);

            var viewModel = new LoginViewModel(token);

            var response = ResultViewModel<LoginViewModel>.Sucess(viewModel);

            return Ok(response);
        }

    }
}
