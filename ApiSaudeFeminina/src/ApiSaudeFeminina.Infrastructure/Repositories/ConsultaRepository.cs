using ApiSaudeFeminina.Domain.Entities;
using ApiSaudeFeminina.Domain.Interfaces;
using ApiSaudeFeminina.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace ApiSaudeFeminina.Infrastructure.Repositories
{
    public class ConsultaRepository : RepositoryBase<Consulta>, IConsultaRepository
    {
        public ConsultaRepository(AppDbContext context) : base(context) { }

        public async Task<IEnumerable<Consulta>> GetByUsuarioIdAsync(Guid usuarioId)
        {
            return await DbSet
                .Where(c => c.UsuarioId == usuarioId)
                .OrderBy(c => c.DataConsulta)
                .ToListAsync();
        }
    }
}
