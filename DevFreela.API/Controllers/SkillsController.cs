using DevFreela.Core.Entities;
using DevFreela.Application.Models;
using DevFreela.Infrastructure.Persistence;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using DevFreela.Application.Interfaces;

namespace DevFreela.API.Controllers
{
    [Route("api/skills")]
    [ApiController]
    public class SkillsController : ControllerBase
    {
        private readonly DevFreelaDbContext _context;
        private readonly ISkillService _service;

        public SkillsController(DevFreelaDbContext context, ISkillService service)
        {
            _context = context;
            _service = service;
        }
        // GET api/skills
        [HttpGet]
        public IActionResult GetAll()
        {
            var result = _service.GetAll();

            if (!result.IsSucess)
            {
                return BadRequest(result.Message);
            }

            return Ok(result);
        }

        // POST api/skills
        [HttpPost]
        public IActionResult Post(CreateSkillInputModel Skillmodel)
        {
            var result = _service.InsereSkill(Skillmodel);

            return NoContent();
        }
    }
}
