using ApiSaudeFeminina.Domain.Entities;
using ApiSaudeFeminina.Domain.Interfaces;
using ApiSaudeFeminina.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace ApiSaudeFeminina.Infrastructure.Repositories
{
    public class RegistroDiarioRepository : RepositoryBase<RegistroDiario>, IRegistroDiarioRepository
    {
        public RegistroDiarioRepository(AppDbContext context) : base(context) { }

        public async Task<IEnumerable<RegistroDiario>> GetByUsuarioIdAsync(Guid usuarioId)
        {
            return await DbSet
                .Where(r => r.UsuarioId == usuarioId)
                .OrderByDescending(r => r.DataRegistro)
                .ToListAsync();
        }

        public async Task<IEnumerable<RegistroDiario>> GetByUsuarioIdAndPeriodoAsync(Guid usuarioId, DateTime dataInicio, DateTime dataFim)
        {
            return await DbSet
                .Where(r => r.UsuarioId == usuarioId && r.DataRegistro >= dataInicio.Date && r.DataRegistro <= dataFim.Date)
                .OrderBy(r => r.DataRegistro)
                .ToListAsync();
        }

        public async Task<RegistroDiario?> GetByUsuarioIdAndDataAsync(Guid usuarioId, DateTime data)
        {
            return await DbSet.FirstOrDefaultAsync(r => r.UsuarioId == usuarioId && r.DataRegistro == data.Date);
        }
    }
}
