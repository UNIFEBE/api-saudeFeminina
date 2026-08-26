using ApiSaudeFeminina.Domain.Entities;
using ApiSaudeFeminina.Domain.Interfaces;
using ApiSaudeFeminina.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace ApiSaudeFeminina.Infrastructure.Repositories
{
    public class CategoriaConteudoRepository : RepositoryBase<CategoriaConteudo>, ICategoriaConteudoRepository
    {
        public CategoriaConteudoRepository(AppDbContext context) : base(context) { }

        public async Task<CategoriaConteudo?> GetByNomeAsync(string nome)
        {
            return await DbSet.FirstOrDefaultAsync(c => c.Nome == nome);
        }
    }
}
