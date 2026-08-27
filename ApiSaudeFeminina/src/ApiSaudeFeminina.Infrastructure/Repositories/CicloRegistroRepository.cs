using ApiSaudeFeminina.Domain.Entities;
using ApiSaudeFeminina.Domain.Interfaces;
using ApiSaudeFeminina.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace ApiSaudeFeminina.Infrastructure.Repositories
{
    public class CicloRegistroRepository : RepositoryBase<CicloRegistro>, ICicloRegistroRepository
    {
        public CicloRegistroRepository(AppDbContext context) : base(context) { }

        public async Task<IEnumerable<CicloRegistro>> GetByUsuarioIdAsync(Guid usuarioId)
        {
            return await DbSet
                .Where(r => r.UsuarioId == usuarioId)
                .OrderByDescending(r => r.DataInicio)
                .ToListAsync();
        }

        public async Task<CicloRegistro?> GetUltimoRegistroAsync(Guid usuarioId)
        {
            return await DbSet
                .Where(r => r.UsuarioId == usuarioId)
                .OrderByDescending(r => r.DataInicio)
                .FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<CicloRegistro>> GetByPeriodoAsync(Guid usuarioId, DateTime dataInicio, DateTime dataFim)
        {
            return await DbSet
                .Where(r => r.UsuarioId == usuarioId && r.DataInicio >= dataInicio && r.DataInicio <= dataFim)
                .OrderByDescending(r => r.DataInicio)
                .ToListAsync();
        }
    }
}
