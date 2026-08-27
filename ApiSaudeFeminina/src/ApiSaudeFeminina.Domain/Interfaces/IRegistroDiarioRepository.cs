using ApiSaudeFeminina.Domain.Entities;

namespace ApiSaudeFeminina.Domain.Interfaces
{
    public interface IRegistroDiarioRepository : IRepository<RegistroDiario>
    {
        Task<IEnumerable<RegistroDiario>> GetByUsuarioIdAsync(Guid usuarioId);
        Task<IEnumerable<RegistroDiario>> GetByUsuarioIdAndPeriodoAsync(Guid usuarioId, DateTime dataInicio, DateTime dataFim);
        Task<RegistroDiario?> GetByUsuarioIdAndDataAsync(Guid usuarioId, DateTime data);
    }
}
