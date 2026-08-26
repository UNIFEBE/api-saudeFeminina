using ApiSaudeFeminina.Domain.Entities;

namespace ApiSaudeFeminina.Domain.Interfaces
{
    public interface ICategoriaConteudoRepository : IRepository<CategoriaConteudo>
    {
        Task<CategoriaConteudo?> GetByNomeAsync(string nome);
    }
}
