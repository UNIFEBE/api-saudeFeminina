using ApiSaudeFeminina.Domain.Entities;

namespace ApiSaudeFeminina.Domain.Interfaces
{
    public interface IArtigoRepository : IRepository<Artigo>
    {
        Task<IEnumerable<Artigo>> GetByCategoriaIdAsync(Guid categoriaId);
        Task<IEnumerable<Artigo>> GetPublicadosAsync();
        Task<IEnumerable<Artigo>> GetAllWithCategoriaAsync();
    }
}
