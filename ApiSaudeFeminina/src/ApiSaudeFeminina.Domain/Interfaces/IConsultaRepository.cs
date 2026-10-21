using ApiSaudeFeminina.Domain.Entities;

namespace ApiSaudeFeminina.Domain.Interfaces
{
    public interface IConsultaRepository : IRepository<Consulta>
    {
        Task<IEnumerable<Consulta>> GetByUsuarioIdAsync(Guid usuarioId);
    }
}
