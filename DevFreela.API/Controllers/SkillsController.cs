using DevFreela.Core.Entities;
using DevFreela.Application.Models;
using DevFreela.Infrastructure.Persistence;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DevFreela.API.Controllers
{
    [Route("api/skills")]
    [ApiController]
    public class SkillsController : ControllerBase
    {
        private readonly DevFreelaDbContext _context;

        public SkillsController(DevFreelaDbContext context)
        {
            _context = context;
        }
        // GET api/skills
        [HttpGet]
        public IActionResult GetAll()
        {
            var skills = _context.Habilidades.ToList();

            if (!skills.Any())
            {
                return NotFound();
            }

            return Ok(skills);
        }

        // POST api/skills
        [HttpPost]
        public IActionResult Post(CreateSkillInputModel Skillmodel)
        {
            var skill = new Habilidade(Skillmodel.Descricao);

            _context.Habilidades.Add(skill);
            _context.SaveChanges();

            return NoContent();
        }
    }
}
