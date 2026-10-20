using ApiSaudeFeminina.Application.DTOs;
using ApiSaudeFeminina.Application.Interfaces;
using ApiSaudeFeminina.Domain.Entities;
using ApiSaudeFeminina.Domain.Interfaces;

namespace ApiSaudeFeminina.Application.Services
{
    public class UsuarioService : IUsuarioService
    {
        private readonly IUsuarioRepository _repository;

        public UsuarioService(IUsuarioRepository repository)
        {
            _repository = repository;
        }

        public async Task<UsuarioDto?> GetByIdAsync(Guid id)
        {
            var usuario = await _repository.GetByIdAsync(id);
            if (usuario is null) return null;

            return MapToDto(usuario);
        }

        public async Task<UsuarioDto?> GetByEmailAsync(string email)
        {
            var usuario = await _repository.GetByEmailAsync(email);
            if (usuario is null) return null;

            return MapToDto(usuario);
        }

        public async Task<IEnumerable<UsuarioDto>> GetAllAsync()
        {
            var usuarios = await _repository.GetAllAsync();
            return usuarios.Select(MapToDto);
        }

        public async Task<UsuarioDto> CreateAsync(CreateUsuarioDto dto)
        {
            // Verifica se o email já existe
            if (await _repository.EmailExistsAsync(dto.Email))
                throw new InvalidOperationException("Email já cadastrado.");

            // Hash da senha (em produção, use BCrypt ou similar)
            var senhaHash = HashPassword(dto.Senha);

            var usuario = new Usuario(
                dto.Nome,
                dto.Email,
                senhaHash,
                dto.DataNascimento,
                dto.Telefone,
                dto.Cidade,
                dto.Estado,
                dto.Peso,
                dto.Altura,
                dto.UsaAnticoncepcional,
                dto.DesejaEngravidar,
                dto.Objetivo);

            await _repository.AddAsync(usuario);
            await _repository.SaveChangesAsync();

            return MapToDto(usuario);
        }

        public async Task<UsuarioDto?> UpdateAsync(Guid id, UpdateUsuarioDto dto)
        {
            var usuario = await _repository.GetByIdAsync(id);
            if (usuario is null) return null;

            // Verifica se o novo email já existe em outro usuário
            if (usuario.Email != dto.Email && await _repository.EmailExistsAsync(dto.Email))
                throw new InvalidOperationException("Email já cadastrado.");

            usuario.Update(
                dto.Nome,
                dto.Email,
                dto.DataNascimento,
                dto.Telefone,
                dto.Cidade,
                dto.Estado,
                dto.UsaAnticoncepcional,
                dto.DesejaEngravidar,
                dto.Objetivo);

            _repository.Update(usuario);
            await _repository.SaveChangesAsync();

            return MapToDto(usuario);
        }

        public async Task<bool> UpdateSenhaAsync(Guid id, UpdateSenhaDto dto)
        {
            var usuario = await _repository.GetByIdAsync(id);
            if (usuario is null) return false;

            // Verifica se a senha atual está correta
            if (!VerifyPassword(dto.SenhaAtual, usuario.SenhaHash))
                throw new InvalidOperationException("Senha atual incorreta.");

            var novaSenhaHash = HashPassword(dto.NovaSenha);
            usuario.UpdateSenha(novaSenhaHash);

            _repository.Update(usuario);
            await _repository.SaveChangesAsync();

            return true;
        }

        public async Task<UsuarioDto?> UpdateDadosSaudeAsync(Guid id, UpdateDadosSaudeDto dto)
        {
            var usuario = await _repository.GetByIdAsync(id);
            if (usuario is null) return null;

            usuario.UpdateDadosSaude(
                dto.Peso,
                dto.Altura,
                dto.TipoSanguineo,
                dto.FaseVida);

            _repository.Update(usuario);
            await _repository.SaveChangesAsync();

            return MapToDto(usuario);
        }

        public async Task<LoginResponseDto?> LoginAsync(LoginDto dto)
        {
            var usuario = await _repository.GetByEmailAsync(dto.Email);
            if (usuario is null) return null;

            if (!VerifyPassword(dto.Senha, usuario.SenhaHash))
                return null;

            return new LoginResponseDto
            {
                Id = usuario.Id,
                Nome = usuario.Nome,
                Email = usuario.Email,
                Message = "Login realizado com sucesso."
            };
        }

        public async Task<bool> DeleteAsync(Guid id)
        {
            var usuario = await _repository.GetByIdAsync(id);
            if (usuario is null) return false;

            _repository.Remove(usuario);
            await _repository.SaveChangesAsync();

            return true;
        }

        private static UsuarioDto MapToDto(Usuario usuario)
        {
            return new UsuarioDto
            {
                Id = usuario.Id,
                Nome = usuario.Nome,
                Email = usuario.Email,
                DataNascimento = usuario.DataNascimento,
                Telefone = usuario.Telefone,
                Cidade = usuario.Cidade,
                Estado = usuario.Estado,
                Peso = usuario.Peso,
                Altura = usuario.Altura,
                TipoSanguineo = usuario.TipoSanguineo,
                FaseVida = usuario.FaseVida,
                UsaAnticoncepcional = usuario.UsaAnticoncepcional,
                DesejaEngravidar = usuario.DesejaEngravidar,
                Objetivo = usuario.Objetivo,
                CreatedAt = usuario.CreatedAt,
                UpdatedAt = usuario.UpdatedAt
            };
        }

        // NOTA: Em produção, use BCrypt, Argon2 ou PBKDF2
        private static string HashPassword(string senha)
        {
            // Implementação simplificada - SUBSTITUIR em produção
            using var sha256 = System.Security.Cryptography.SHA256.Create();
            var bytes = System.Text.Encoding.UTF8.GetBytes(senha);
            var hash = sha256.ComputeHash(bytes);
            return Convert.ToBase64String(hash);
        }

        private static bool VerifyPassword(string senha, string senhaHash)
        {
            return HashPassword(senha) == senhaHash;
        }
    }
}
