using Azure.Core;
using DevFreela.Core.Entities;
using DevFreela.Core.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DevFreela.Infrastructure.Persistence.Repositories
{
    public class ProjectRepository : IProjectRepository
    {
        private readonly DevFreelaDbContext _db;
        public ProjectRepository(DevFreelaDbContext db)
        {
            _db = db;
        }

        public async Task AddComment(Comentario comentario)
        {
            await _db.Comentarios.AddAsync(comentario);
            await _db.SaveChangesAsync();
        }

        public async Task<int> AddProject(Project project)
        {
            await _db.Projects.AddAsync(project);
            await _db.SaveChangesAsync();

            return project.Id;
        }

        public async Task Complete(Project project)
        {
            _db.Projects.Update(project);
            await _db.SaveChangesAsync();
        }

        public async Task Delete(Project project)
        {
            _db.Projects.Update(project);
            await _db.SaveChangesAsync();
        }

        public async Task<bool> Exists(int id)
        {
            return await _db.Projects.AnyAsync(p => p.Id == id);
        }

        public async Task<List<Project>> GetAll()
        {
            return await _db.Projects
                .Include(p => p.Cliente)
                .Include(p => p.Freelancer)
                .ToListAsync();
        }

        public async Task<List<Project>> GetAllFiltro(string search, int page, int tamanho)
        {
            return await _db.Projects
                .Include(p => p.Cliente)
                .Include(p => p.Freelancer)
                .Where(p => !p.IsDeleted && (search == "" || p.Title.Contains(search) || p.Description.Contains(search)))
                .Skip(page * tamanho)
                .Take(tamanho)
                .ToListAsync();
        }

        public async Task<Project?> GetById(int id)
        {
            return await _db.Projects.SingleOrDefaultAsync(p => p.Id == id);
        }

        public async Task<Project?> GetDetailsById(int id)
        {
            return await _db.Projects
                .Include(p => p.Cliente)
                .Include(p => p.Freelancer)
                .Include(p => p.Comentarios)
                .FirstOrDefaultAsync(p => p.Id == id);
        }

        public async Task Start(Project project)
        {
            _db.Projects.Update(project);
            await _db.SaveChangesAsync();
        }

        public async Task Update(Project project)
        {
            _db.Projects.Update(project);
            await _db.SaveChangesAsync();
        }
    }
}
