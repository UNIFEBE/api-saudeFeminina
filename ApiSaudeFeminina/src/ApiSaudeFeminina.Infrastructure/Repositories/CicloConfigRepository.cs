using ApiSaudeFeminina.Domain.Entities;
using ApiSaudeFeminina.Domain.Interfaces;
using ApiSaudeFeminina.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace ApiSaudeFeminina.Infrastructure.Repositories
{
    public class CicloConfigRepository : RepositoryBase<CicloConfig>, ICicloConfigRepository
    {
        public CicloConfigRepository(AppDbContext context) : base(context) { }

        public async Task<CicloConfig?> GetByUsuarioIdAsync(Guid usuarioId)
        {
            return await DbSet.FirstOrDefaultAsync(c => c.UsuarioId == usuarioId);
        }
    }
}
