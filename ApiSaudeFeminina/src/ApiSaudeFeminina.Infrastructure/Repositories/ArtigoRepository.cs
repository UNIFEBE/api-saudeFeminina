using ApiSaudeFeminina.Domain.Entities;
using ApiSaudeFeminina.Domain.Interfaces;
using ApiSaudeFeminina.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace ApiSaudeFeminina.Infrastructure.Repositories
{
    public class ArtigoRepository : RepositoryBase<Artigo>, IArtigoRepository
    {
        public ArtigoRepository(AppDbContext context) : base(context) { }

        public async Task<IEnumerable<Artigo>> GetByCategoriaIdAsync(Guid categoriaId)
        {
            return await DbSet
                .Include(a => a.Categoria)
                .Where(a => a.CategoriaId == categoriaId)
                .OrderByDescending(a => a.CreatedAt)
                .ToListAsync();
        }

        public async Task<IEnumerable<Artigo>> GetPublicadosAsync()
        {
            return await DbSet
                .Include(a => a.Categoria)
                .Where(a => a.Publicado)
                .OrderByDescending(a => a.CreatedAt)
                .ToListAsync();
        }

        public async Task<IEnumerable<Artigo>> GetAllWithCategoriaAsync()
        {
            return await DbSet
                .Include(a => a.Categoria)
                .OrderByDescending(a => a.CreatedAt)
                .ToListAsync();
        }
    }
}
