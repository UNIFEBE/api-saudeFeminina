using ApiSaudeFeminina.Application.DTOs;

namespace ApiSaudeFeminina.Application.Interfaces
{
    public interface ICicloRegistroService
    {
        Task<IEnumerable<CicloRegistroDto>> GetAllByUsuarioAsync(Guid usuarioId);
        Task<CicloRegistroDto?> GetByIdAsync(Guid id);
        Task<CicloRegistroDto> CreateAsync(CreateCicloRegistroDto dto);
        Task<CicloRegistroDto?> UpdateAsync(Guid id, UpdateCicloRegistroDto dto);
        Task<CicloRegistroDto?> FinalizarPeriodoAsync(Guid id, DateTime dataFim);
        Task<bool> DeleteAsync(Guid id);
        Task<PrevisaoCicloDto> GetPrevisaoAsync(Guid usuarioId);
    }
}
