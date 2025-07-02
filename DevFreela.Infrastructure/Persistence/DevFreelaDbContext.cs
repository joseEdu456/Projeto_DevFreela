using DevFreela.Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace DevFreela.Infrastructure.Persistence
{
    public class DevFreelaDbContext : DbContext
    {
        public DevFreelaDbContext(DbContextOptions options) : base(options)
        {

        }

        public DbSet<Project> Projects { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Habilidade> Habilidades { get; set; }
        public DbSet<UserHabilidade> UserHabilidades { get; set; }
        public DbSet<Comentario> Comentarios { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<Habilidade>(e =>
            {
                e.HasKey(s => s.Id);
            });

            builder.Entity<UserHabilidade>(e =>
            {
                e.HasKey(us => us.Id);

                e.HasOne(u => u.Habilidade)
                    .WithMany(u => u.UserHabilidades)
                    .HasForeignKey(u => u.IdHabilidade)
                    .OnDelete(DeleteBehavior.Restrict);
            });

            builder.Entity<Comentario>(e =>
            {
                e.HasKey(c => c.Id);

                e.HasOne(p => p.Projeto)
                    .WithMany(p => p.Comentarios)
                    .HasForeignKey(p => p.IdProjeto)
                    .OnDelete(DeleteBehavior.Restrict);
            });

            builder.Entity<User>(e =>
            {
                e.HasKey(u => u.Id);

                e.HasMany(u => u.Comentarios)
                    .WithOne(u => u.User)
                    .HasForeignKey(u => u.IdUser)
                    .OnDelete(DeleteBehavior.Restrict);
            });

            builder.Entity<Project>(e =>
            {
                e.HasKey(p => p.Id);

                e.HasOne(p => p.Freelancer)
                    .WithMany(f => f.FreelanceProjetos)
                    .HasForeignKey(f => f.IdFreelancer)
                    .OnDelete(DeleteBehavior.Restrict);

                e.HasOne(p => p.Cliente)
                    .WithMany(c => c.MeusProjetos)
                    .HasForeignKey(c => c.IdCliente)
                    .OnDelete(DeleteBehavior.Restrict);
            });
            
            base.OnModelCreating(builder);
        }
    }
}
