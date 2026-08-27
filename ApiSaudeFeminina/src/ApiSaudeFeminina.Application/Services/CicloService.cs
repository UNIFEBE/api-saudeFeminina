using ApiSaudeFeminina.Application.DTOs;
using ApiSaudeFeminina.Application.Interfaces;
using ApiSaudeFeminina.Domain.Entities;
using ApiSaudeFeminina.Domain.Interfaces;

namespace ApiSaudeFeminina.Application.Services
{
    public class CicloService : ICicloService
    {
        private readonly ICicloConfigRepository _cicloConfigRepo;
        private readonly IRegistroDiarioRepository _registroDiarioRepo;

        public CicloService(ICicloConfigRepository cicloConfigRepo, IRegistroDiarioRepository registroDiarioRepo)
        {
            _cicloConfigRepo = cicloConfigRepo;
            _registroDiarioRepo = registroDiarioRepo;
        }

        // ── Configuração do Ciclo ──────────────────────────────────────

        public async Task<CicloConfigDto?> GetConfigByUsuarioIdAsync(Guid usuarioId)
        {
            var config = await _cicloConfigRepo.GetByUsuarioIdAsync(usuarioId);
            if (config is null) return null;

            return MapConfigToDto(config);
        }

        public async Task<CicloConfigDto> CreateOrUpdateConfigAsync(CreateCicloConfigDto dto)
        {
            var existing = await _cicloConfigRepo.GetByUsuarioIdAsync(dto.UsuarioId);

            if (existing is not null)
            {
                existing.Update(dto.DataUltimaMenstruacao, dto.DuracaoCiclo, dto.DuracaoMenstruacao, dto.CicloRegular);
                _cicloConfigRepo.Update(existing);
                await _cicloConfigRepo.SaveChangesAsync();
                return MapConfigToDto(existing);
            }

            var config = new CicloConfig(
                dto.UsuarioId,
                dto.DataUltimaMenstruacao,
                dto.DuracaoCiclo,
                dto.DuracaoMenstruacao,
                dto.CicloRegular);

            await _cicloConfigRepo.AddAsync(config);
            await _cicloConfigRepo.SaveChangesAsync();

            return MapConfigToDto(config);
        }

        public async Task<CicloConfigDto?> UpdateConfigAsync(Guid usuarioId, UpdateCicloConfigDto dto)
        {
            var config = await _cicloConfigRepo.GetByUsuarioIdAsync(usuarioId);
            if (config is null) return null;

            config.Update(dto.DataUltimaMenstruacao, dto.DuracaoCiclo, dto.DuracaoMenstruacao, dto.CicloRegular);
            _cicloConfigRepo.Update(config);
            await _cicloConfigRepo.SaveChangesAsync();

            return MapConfigToDto(config);
        }

        // ── Registros Diários ──────────────────────────────────────────

        public async Task<IEnumerable<RegistroDiarioDto>> GetRegistrosByUsuarioIdAsync(Guid usuarioId)
        {
            var registros = await _registroDiarioRepo.GetByUsuarioIdAsync(usuarioId);
            return registros.Select(MapRegistroToDto);
        }

        public async Task<IEnumerable<RegistroDiarioDto>> GetRegistrosByPeriodoAsync(Guid usuarioId, DateTime dataInicio, DateTime dataFim)
        {
            var registros = await _registroDiarioRepo.GetByUsuarioIdAndPeriodoAsync(usuarioId, dataInicio, dataFim);
            return registros.Select(MapRegistroToDto);
        }

        public async Task<RegistroDiarioDto?> GetRegistroByDataAsync(Guid usuarioId, DateTime data)
        {
            var registro = await _registroDiarioRepo.GetByUsuarioIdAndDataAsync(usuarioId, data);
            if (registro is null) return null;

            return MapRegistroToDto(registro);
        }

        public async Task<RegistroDiarioDto> CreateOrUpdateRegistroAsync(CreateRegistroDiarioDto dto)
        {
            var existing = await _registroDiarioRepo.GetByUsuarioIdAndDataAsync(dto.UsuarioId, dto.DataRegistro.Date);

            if (existing is not null)
            {
                existing.Update(dto.Humor, dto.Fluxo, dto.Temperatura, dto.Sintomas, dto.Observacoes);
                _registroDiarioRepo.Update(existing);
                await _registroDiarioRepo.SaveChangesAsync();
                return MapRegistroToDto(existing);
            }

            var registro = new RegistroDiario(
                dto.UsuarioId,
                dto.DataRegistro,
                dto.Humor,
                dto.Fluxo,
                dto.Temperatura,
                dto.Sintomas,
                dto.Observacoes);

            await _registroDiarioRepo.AddAsync(registro);
            await _registroDiarioRepo.SaveChangesAsync();

            return MapRegistroToDto(registro);
        }

        public async Task<bool> DeleteRegistroAsync(Guid id)
        {
            var registro = await _registroDiarioRepo.GetByIdAsync(id);
            if (registro is null) return false;

            _registroDiarioRepo.Remove(registro);
            await _registroDiarioRepo.SaveChangesAsync();
            return true;
        }

        // ── Mapeamentos ────────────────────────────────────────────────

        private static CicloConfigDto MapConfigToDto(CicloConfig config)
        {
            var proximoCiclo = config.CalcularProximoCiclo();
            var diasRestantes = (proximoCiclo - DateTime.UtcNow.Date).Days;

            return new CicloConfigDto
            {
                Id = config.Id,
                UsuarioId = config.UsuarioId,
                DataUltimaMenstruacao = config.DataUltimaMenstruacao,
                DuracaoCiclo = config.DuracaoCiclo,
                DuracaoMenstruacao = config.DuracaoMenstruacao,
                CicloRegular = config.CicloRegular,
                UpdatedAt = config.UpdatedAt,
                ProximoCiclo = proximoCiclo,
                InicioJanelaFertil = config.CalcularInicioJanelaFertil(),
                DiasParaProximoCiclo = diasRestantes
            };
        }

        private static RegistroDiarioDto MapRegistroToDto(RegistroDiario registro)
        {
            return new RegistroDiarioDto
            {
                Id = registro.Id,
                UsuarioId = registro.UsuarioId,
                DataRegistro = registro.DataRegistro,
                Humor = registro.Humor,
                Fluxo = registro.Fluxo,
                Temperatura = registro.Temperatura,
                Sintomas = registro.Sintomas,
                Observacoes = registro.Observacoes,
                CreatedAt = registro.CreatedAt
            };
        }
    }
}
