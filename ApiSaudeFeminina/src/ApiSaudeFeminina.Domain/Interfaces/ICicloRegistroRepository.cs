using ApiSaudeFeminina.Domain.Entities;

namespace ApiSaudeFeminina.Domain.Interfaces
{
    public interface ICicloRegistroRepository : IRepository<CicloRegistro>
    {
        Task<IEnumerable<CicloRegistro>> GetByUsuarioIdAsync(Guid usuarioId);
        Task<CicloRegistro?> GetUltimoRegistroAsync(Guid usuarioId);
        Task<IEnumerable<CicloRegistro>> GetByPeriodoAsync(Guid usuarioId, DateTime dataInicio, DateTime dataFim);
    }
}
