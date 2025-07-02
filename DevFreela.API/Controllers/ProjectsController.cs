using DevFreela.Core.Entities;
using DevFreela.Application.Models;
using DevFreela.Infrastructure.Persistence;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace DevFreela.API.Controllers
{
    [Route("api/projects")]
    [ApiController]
    public class ProjectsController : ControllerBase
    {
        private readonly DevFreelaDbContext _db;
        public ProjectsController(DevFreelaDbContext db)
        {
            _db = db;
        }

        // GET api/projects?search=crm
        [HttpGet]
        public IActionResult Get(string search = "", int page = 0, int tamanho = 3)
        {
            var projetos = _db.Projects
                .Include(p => p.Cliente)
                .Include(p => p.Freelancer)
                .Where(p => !p.IsDeleted && (search == "" || p.Title.Contains(search) || p.Description.Contains(search)))
                .Skip(page * tamanho)
                .Take(tamanho)
                .ToList();

            var model = projetos.Select(ProjectItemViewModel.ToEntity).ToList();

            return Ok(model);
        }

        // GET api/projects/1234
        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var projeto = _db.Projects
                .Include(p => p.Cliente)
                .Include(p => p.Freelancer)
                .Include(p => p.Comentarios)
                .SingleOrDefault(p => p.Id == id);

            var model = ProjectViewModel.FromEntidade(projeto);

            return Ok(model);
        }

        // POST api/projects
        [HttpPost]
        public IActionResult Post(CreateProjectInputModel model)
        {
            var projeto = model.ToEntity();

            _db.Projects.Add(projeto);
            _db.SaveChanges();

            return CreatedAtAction(nameof(GetById), new { id = 1 }, model);
        }

        // PUT api/project/1234
        [HttpPut("{id}")]
        public IActionResult Put(int id, UpdateProjectInputModel model)
        {
            var projeto = _db.Projects.SingleOrDefault(p => p.Id == id);

            if(projeto is null)
            {
                return NotFound();
            }

            projeto.AtualizarDados(model.Title, model.Descricao, model.TotalCost);

            _db.Projects.Update(projeto);
            _db.SaveChanges();

            return NoContent();
        }

        //DELETE api/projects/1234
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var projeto = _db.Projects.SingleOrDefault(p => p.Id == id);

            if (projeto is null)
            {
                return NotFound();
            }

            projeto.SetAsDeleted();

            _db.Projects.Update(projeto);
            _db.SaveChanges();

            return NoContent();
        }

        // PUT api/projects/1234/start
        [HttpPut("{id}/start")]
        public IActionResult Start(int id)
        {
            var projeto = _db.Projects.SingleOrDefault(p => p.Id == id);

            if (projeto is null)
            {
                return NotFound();
            }

            projeto.Iniciar();

            _db.Projects.Update(projeto);
            _db.SaveChanges();

            return NoContent();
        }

        // PUT api/projects/1234/complete
        [HttpPut("{id}/complete")]
        public IActionResult Complete(int id)
        {
            var projeto = _db.Projects.SingleOrDefault(p => p.Id == id);

            if (projeto is null)
            {
                return NotFound();
            }

            projeto.Finalizar();

            _db.Projects.Update(projeto);
            _db.SaveChanges();

            return NoContent();
        }

        // POST api/projects/1234/comments
        [HttpPost("{id}/comments")]
        public IActionResult PostComment(int id, CreateProjectCommentInputModel model)
        {
            var projeto = _db.Projects.SingleOrDefault(p => p.Id == id);

            if (projeto is null)
            {
                return NotFound();
            }

            var comentario = new Comentario(model.Text, model.IdProject, model.IdUser);

            _db.Comentarios.Add(comentario);
            _db.SaveChanges();

            return Ok();
        }
    }
}
