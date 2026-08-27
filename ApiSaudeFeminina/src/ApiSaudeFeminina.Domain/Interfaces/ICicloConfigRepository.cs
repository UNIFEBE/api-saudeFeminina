using ApiSaudeFeminina.Domain.Entities;

namespace ApiSaudeFeminina.Domain.Interfaces
{
    public interface ICicloConfigRepository : IRepository<CicloConfig>
    {
        Task<CicloConfig?> GetByUsuarioIdAsync(Guid usuarioId);
    }
}
