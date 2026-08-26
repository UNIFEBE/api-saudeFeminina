using ApiSaudeFeminina.Application.DTOs;
using ApiSaudeFeminina.Application.Interfaces;
using ApiSaudeFeminina.Domain.Entities;
using ApiSaudeFeminina.Domain.Interfaces;

namespace ApiSaudeFeminina.Application.Services
{
    public class ArtigoService : IArtigoService
    {
        private readonly IArtigoRepository _artigoRepository;
        private readonly ICategoriaConteudoRepository _categoriaRepository;

        public ArtigoService(IArtigoRepository artigoRepository, ICategoriaConteudoRepository categoriaRepository)
        {
            _artigoRepository = artigoRepository;
            _categoriaRepository = categoriaRepository;
        }

        public async Task<ArtigoDto?> GetByIdAsync(Guid id)
        {
            var artigo = await _artigoRepository.GetByIdAsync(id);
            if (artigo is null) return null;

            return MapToDto(artigo);
        }

        public async Task<IEnumerable<ArtigoDto>> GetAllAsync()
        {
            var artigos = await _artigoRepository.GetAllWithCategoriaAsync();
            return artigos.Select(MapToDto);
        }

        public async Task<IEnumerable<ArtigoDto>> GetByCategoriaIdAsync(Guid categoriaId)
        {
            var artigos = await _artigoRepository.GetByCategoriaIdAsync(categoriaId);
            return artigos.Select(MapToDto);
        }

        public async Task<IEnumerable<ArtigoDto>> GetPublicadosAsync()
        {
            var artigos = await _artigoRepository.GetPublicadosAsync();
            return artigos.Select(MapToDto);
        }

        public async Task<IEnumerable<CategoriaConteudoDto>> GetCategoriasAsync()
        {
            var categorias = await _categoriaRepository.GetAllAsync();
            return categorias.Select(c => new CategoriaConteudoDto
            {
                Id = c.Id,
                Nome = c.Nome,
                Icone = c.Icone
            });
        }

        public async Task<ArtigoDto> CreateAsync(CreateArtigoDto dto)
        {
            var artigo = new Artigo(
                dto.CategoriaId,
                dto.Titulo,
                dto.Resumo,
                dto.Conteudo,
                dto.Publicado);

            await _artigoRepository.AddAsync(artigo);
            await _artigoRepository.SaveChangesAsync();

            return MapToDto(artigo);
        }

        public async Task<ArtigoDto?> UpdateAsync(Guid id, UpdateArtigoDto dto)
        {
            var artigo = await _artigoRepository.GetByIdAsync(id);
            if (artigo is null) return null;

            artigo.Update(
                dto.CategoriaId,
                dto.Titulo,
                dto.Resumo,
                dto.Conteudo,
                dto.Publicado);

            _artigoRepository.Update(artigo);
            await _artigoRepository.SaveChangesAsync();

            return MapToDto(artigo);
        }

        public async Task<bool> DeleteAsync(Guid id)
        {
            var artigo = await _artigoRepository.GetByIdAsync(id);
            if (artigo is null) return false;

            _artigoRepository.Remove(artigo);
            await _artigoRepository.SaveChangesAsync();

            return true;
        }

        private static ArtigoDto MapToDto(Artigo artigo)
        {
            return new ArtigoDto
            {
                Id = artigo.Id,
                CategoriaId = artigo.CategoriaId,
                CategoriaNome = artigo.Categoria?.Nome ?? string.Empty,
                Titulo = artigo.Titulo,
                Resumo = artigo.Resumo,
                Conteudo = artigo.Conteudo,
                Publicado = artigo.Publicado,
                CreatedAt = artigo.CreatedAt,
                UpdatedAt = artigo.UpdatedAt
            };
        }
    }
}
