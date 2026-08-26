using ApiSaudeFeminina.Application.DTOs;

namespace ApiSaudeFeminina.Application.Interfaces
{
    public interface IUsuarioService
    {
        Task<UsuarioDto?> GetByIdAsync(Guid id);
        Task<UsuarioDto?> GetByEmailAsync(string email);
        Task<IEnumerable<UsuarioDto>> GetAllAsync();
        Task<UsuarioDto> CreateAsync(CreateUsuarioDto dto);
        Task<UsuarioDto?> UpdateAsync(Guid id, UpdateUsuarioDto dto);
        Task<bool> UpdateSenhaAsync(Guid id, UpdateSenhaDto dto);
        Task<UsuarioDto?> UpdateDadosSaudeAsync(Guid id, UpdateDadosSaudeDto dto);
        Task<bool> DeleteAsync(Guid id);
        Task<LoginResponseDto?> LoginAsync(LoginDto dto);
    }
}
