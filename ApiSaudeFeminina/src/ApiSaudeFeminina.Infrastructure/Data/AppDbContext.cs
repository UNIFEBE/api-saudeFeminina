using ApiSaudeFeminina.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace ApiSaudeFeminina.Infrastructure.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<Product> Products => Set<Product>();
        public DbSet<Usuario> Usuarios => Set<Usuario>();
        public DbSet<Artigo> Artigos => Set<Artigo>();
        public DbSet<CategoriaConteudo> CategoriasConteudo => Set<CategoriaConteudo>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(AppDbContext).Assembly);
            base.OnModelCreating(modelBuilder);
        }
    }
}
