using DevFreela.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DevFreela.Core.Repositories
{
    public interface IProjectRepository
    {
        Task<Project?> GetById(int id);
        Task<Project?> GetDetailsById(int id);
        Task<List<Project>> GetAll();
        Task<List<Project>> GetAllFiltro(string search, int page, int tamanho);
        Task<int> AddProject(Project project);
        Task Update(Project project);
        Task Complete(Project project);
        Task Delete(Project project);
        Task Start(Project project);
        Task AddComment(Comentario comentario);
        Task<bool> Exists(int id);
    }
}
