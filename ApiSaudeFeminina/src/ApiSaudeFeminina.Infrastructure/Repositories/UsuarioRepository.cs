using ApiSaudeFeminina.Domain.Entities;
using ApiSaudeFeminina.Domain.Interfaces;
using ApiSaudeFeminina.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace ApiSaudeFeminina.Infrastructure.Repositories
{
    public class UsuarioRepository : RepositoryBase<Usuario>, IUsuarioRepository
    {
        public UsuarioRepository(AppDbContext context) : base(context) { }

        public async Task<Usuario?> GetByEmailAsync(string email)
        {
            return await DbSet.FirstOrDefaultAsync(u => u.Email == email.ToLower());
        }

        public async Task<bool> EmailExistsAsync(string email)
        {
            return await DbSet.AnyAsync(u => u.Email == email.ToLower());
        }
    }
}
