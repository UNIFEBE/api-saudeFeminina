using ApiSaudeFeminina.Application.DTOs;

namespace ApiSaudeFeminina.Application.Interfaces
{
    public interface ICicloService
    {
        // Configuração do ciclo
        Task<CicloConfigDto?> GetConfigByUsuarioIdAsync(Guid usuarioId);
        Task<CicloConfigDto> CreateOrUpdateConfigAsync(CreateCicloConfigDto dto);
        Task<CicloConfigDto?> UpdateConfigAsync(Guid usuarioId, UpdateCicloConfigDto dto);

        // Registros diários
        Task<IEnumerable<RegistroDiarioDto>> GetRegistrosByUsuarioIdAsync(Guid usuarioId);
        Task<IEnumerable<RegistroDiarioDto>> GetRegistrosByPeriodoAsync(Guid usuarioId, DateTime dataInicio, DateTime dataFim);
        Task<RegistroDiarioDto?> GetRegistroByDataAsync(Guid usuarioId, DateTime data);
        Task<RegistroDiarioDto> CreateOrUpdateRegistroAsync(CreateRegistroDiarioDto dto);
        Task<bool> DeleteRegistroAsync(Guid id);
    }
}
