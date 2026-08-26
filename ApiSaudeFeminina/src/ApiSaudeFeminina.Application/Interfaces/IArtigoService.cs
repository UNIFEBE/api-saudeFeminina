using ApiSaudeFeminina.Application.DTOs;

namespace ApiSaudeFeminina.Application.Interfaces
{
    public interface IArtigoService
    {
        Task<ArtigoDto?> GetByIdAsync(Guid id);
        Task<IEnumerable<ArtigoDto>> GetAllAsync();
        Task<IEnumerable<ArtigoDto>> GetByCategoriaIdAsync(Guid categoriaId);
        Task<IEnumerable<ArtigoDto>> GetPublicadosAsync();
        Task<IEnumerable<CategoriaConteudoDto>> GetCategoriasAsync();
        Task<ArtigoDto> CreateAsync(CreateArtigoDto dto);
        Task<ArtigoDto?> UpdateAsync(Guid id, UpdateArtigoDto dto);
        Task<bool> DeleteAsync(Guid id);
    }
}
