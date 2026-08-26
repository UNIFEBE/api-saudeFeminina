using ApiSaudeFeminina.Application.DTOs;

namespace ApiSaudeFeminina.Application.Interfaces
{
    public interface IProductService
    {
        Task<ProductDto?> GetByIdAsync(Guid id);
        Task<IEnumerable<ProductDto>> GetAllAsync();
        Task<ProductDto> CreateAsync(CreateProductDto dto);
    }
}
