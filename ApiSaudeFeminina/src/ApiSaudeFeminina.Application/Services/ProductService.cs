using ApiSaudeFeminina.Application.DTOs;
using ApiSaudeFeminina.Application.Interfaces;
using ApiSaudeFeminina.Domain.Entities;
using ApiSaudeFeminina.Domain.Interfaces;

namespace ApiSaudeFeminina.Application.Services
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository _repository;

        public ProductService(IProductRepository repository)
        {
            _repository = repository;
        }

        public async Task<ProductDto?> GetByIdAsync(Guid id)
        {
            var product = await _repository.GetByIdAsync(id);
            if (product is null) return null;

            return MapToDto(product);
        }

        public async Task<IEnumerable<ProductDto>> GetAllAsync()
        {
            var products = await _repository.GetAllAsync();
            return products.Select(MapToDto);
        }

        public async Task<ProductDto> CreateAsync(CreateProductDto dto)
        {
            var product = new Product(dto.Name, dto.Price, dto.StockQuantity);

            await _repository.AddAsync(product);
            await _repository.SaveChangesAsync();

            return MapToDto(product);
        }

        private static ProductDto MapToDto(Product product)
        {
            var dto = new ProductDto();
            dto.Id = product.Id;
            dto.Name = product.Name;
            dto.Price = product.Price;
            dto.StockQuantity = product.StockQuantity;
            return dto;
        }
    }
}
