using DevFreela.Application.Models;
using DevFreela.Core.Entities;
using DevFreela.Core.Interfaces;
using DevFreela.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DevFreela.Application.Services
{
    public class ProjectService : IProjectService
    {
        private readonly DevFreelaDbContext _db;
        public ProjectService(DevFreelaDbContext db)
        {
            _db = db;
        }

        public ResultViewModel Complete(int id)
        {
            var projeto = _db.Projects.SingleOrDefault(p => p.Id == id);

            if (projeto is null)
            {
                return ResultViewModel.Error("Projeto não encontrado");
            }

            projeto.Finalizar();

            _db.Projects.Update(projeto);
            _db.SaveChanges();

            return ResultViewModel.Sucess();
        }

        public ResultViewModel Delete(int id)
        {
            var projeto = _db.Projects.SingleOrDefault(p => p.Id == id);

            if (projeto is null)
            {
                return ResultViewModel.Error("Projeto não encontrado");
            }

            projeto.SetAsDeleted();

            _db.Projects.Update(projeto);
            _db.SaveChanges();

            return ResultViewModel.Sucess();
        }

        public ResultViewModel<List<ProjectItemViewModel>> GetAll(string search = "", int page = 0, int tamanho = 3)
        {
            var projetos = _db.Projects
                .Include(p => p.Cliente)
                .Include(p => p.Freelancer)
                .Where(p => !p.IsDeleted && (search == "" || p.Title.Contains(search) || p.Description.Contains(search)))
                .Skip(page * tamanho)
                .Take(tamanho)
                .ToList();

            var model = projetos.Select(ProjectItemViewModel.ToEntity).ToList();

            return ResultViewModel<List<ProjectItemViewModel>>.Sucess(model);
        }

        public ResultViewModel<ProjectViewModel> GetById(int id)
        {
            var projeto = _db.Projects
                .Include(p => p.Cliente)
                .Include(p => p.Freelancer)
                .Include(p => p.Comentarios)
                .SingleOrDefault(p => p.Id == id);

            if (projeto == null)
                return ResultViewModel<ProjectViewModel>.Error("Projeto não encontrado");

            var model = ProjectViewModel.FromEntidade(projeto);

            return ResultViewModel<ProjectViewModel>.Sucess(model);
        }

        public ResultViewModel<int> Insert(CreateProjectInputModel model)
        {
            var projeto = model.ToEntity();

            _db.Projects.Add(projeto);
            _db.SaveChanges();

            return ResultViewModel<int>.Sucess(projeto.Id);
        }

        public ResultViewModel InsertComment(int id, CreateProjectCommentInputModel model)
        {
            var projeto = _db.Projects.SingleOrDefault(p => p.Id == id);

            if (projeto is null)
            {
                return ResultViewModel.Error("Projeto não encontrado");
            }

            var comentario = new Comentario(model.Text, model.IdProject, model.IdUser);

            _db.Comentarios.Add(comentario);
            _db.SaveChanges();

            return ResultViewModel.Sucess();
        }

        public ResultViewModel Start(int id)
        {
            var projeto = _db.Projects.SingleOrDefault(p => p.Id == id);

            if (projeto is null)
            {
                return ResultViewModel.Error("Projeto não encontrado");
            }

            projeto.Iniciar();

            _db.Projects.Update(projeto);
            _db.SaveChanges();

            return ResultViewModel.Sucess();
        }

        public ResultViewModel Update(int id, UpdateProjectInputModel model)
        {
            var projeto = _db.Projects.SingleOrDefault(p => p.Id == id);

            if (projeto is null)
            {
                return ResultViewModel.Error("Projeto não encontrado");
            }

            projeto.AtualizarDados(model.Title, model.Descricao, model.TotalCost);

            _db.Projects.Update(projeto);
            _db.SaveChanges();

            return ResultViewModel.Sucess();
        }
    }
}
