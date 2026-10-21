using ApiSaudeFeminina.Application.DTOs;

namespace ApiSaudeFeminina.Application.Interfaces
{
    public interface IConsultaService
    {
        Task<IEnumerable<ConsultaDto>> GetByUsuarioIdAsync(Guid usuarioId);
        Task<ConsultaDto?> GetByIdAsync(Guid id);
        Task<ConsultaDto> CreateAsync(CreateConsultaDto dto);
        Task<ConsultaDto?> UpdateStatusAsync(Guid id, UpdateConsultaStatusDto dto);
        Task<bool> DeleteAsync(Guid id);
    }
}
