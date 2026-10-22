using ApiSaudeFeminina.Application.DTOs;
using ApiSaudeFeminina.Application.Interfaces;
using ApiSaudeFeminina.Domain.Entities;
using ApiSaudeFeminina.Domain.Interfaces;

namespace ApiSaudeFeminina.Application.Services
{
    public class CicloRegistroService : ICicloRegistroService
    {
        private readonly ICicloRegistroRepository _repository;

        public CicloRegistroService(ICicloRegistroRepository repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<CicloRegistroDto>> GetAllByUsuarioAsync(Guid usuarioId)
        {
            var registros = await _repository.GetByUsuarioIdAsync(usuarioId);
            return registros.Select(MapToDto);
        }

        public async Task<CicloRegistroDto?> GetByIdAsync(Guid id)
        {
            var registro = await _repository.GetByIdAsync(id);
            if (registro is null) return null;
            return MapToDto(registro);
        }

        public async Task<CicloRegistroDto> CreateAsync(CreateCicloRegistroDto dto)
        {
            var registro = new CicloRegistro(dto.UsuarioId, dto.DataInicio, dto.DataFim, dto.Observacoes);

            // Busca o registro mais recente ANTES do novo (ordenado DESC, então é o primeiro)
            var anterior = await _repository.GetUltimoRegistroAsync(dto.UsuarioId);

            if (anterior is not null && dto.DataInicio > anterior.DataInicio)
            {
                int diasEntre = (dto.DataInicio - anterior.DataInicio).Days;

                // Salva a duração no registro anterior (intervalo entre o início dele e o início do novo)
                if (diasEntre >= 1)
                {
                    anterior.SetDuracaoCiclo(diasEntre);
                    _repository.Update(anterior);
                }

                // Salva também no novo registro para que o cálculo de média funcione imediatamente
                if (diasEntre >= 1)
                    registro.SetDuracaoCiclo(diasEntre);
            }

            await _repository.AddAsync(registro);
            await _repository.SaveChangesAsync();

            return MapToDto(registro);
        }

        public async Task<CicloRegistroDto?> UpdateAsync(Guid id, UpdateCicloRegistroDto dto)
        {
            var registro = await _repository.GetByIdAsync(id);
            if (registro is null) return null;

            registro.Update(dto.DataInicio, dto.DataFim, dto.Observacoes);
            _repository.Update(registro);
            await _repository.SaveChangesAsync();

            return MapToDto(registro);
        }

        public async Task<CicloRegistroDto?> FinalizarPeriodoAsync(Guid id, DateTime dataFim)
        {
            var registro = await _repository.GetByIdAsync(id);
            if (registro is null) return null;

            registro.FinalizarPeriodo(dataFim);
            _repository.Update(registro);
            await _repository.SaveChangesAsync();

            return MapToDto(registro);
        }

        public async Task<bool> DeleteAsync(Guid id)
        {
            var registro = await _repository.GetByIdAsync(id);
            if (registro is null) return false;

            _repository.Remove(registro);
            await _repository.SaveChangesAsync();
            return true;
        }

        public async Task<PrevisaoCicloDto> GetPrevisaoAsync(Guid usuarioId)
        {
            var registros = (await _repository.GetByUsuarioIdAsync(usuarioId)).ToList();

            var previsao = new PrevisaoCicloDto
            {
                TotalRegistros = registros.Count,
                UltimosRegistros = registros.Take(5).Select(MapToDto).ToList()
            };

            if (registros.Count == 0)
            {
                previsao.CicloMedio = 28;
                previsao.DuracaoMediaPeriodo = 5;
                return previsao;
            }

            // Calcula ciclo médio a partir dos registros que têm duração de ciclo
            var comDuracao = registros.Where(r => r.DuracaoCiclo.HasValue).ToList();
            previsao.CicloMedio = comDuracao.Count > 0
                ? (int)Math.Round(comDuracao.Average(r => r.DuracaoCiclo!.Value))
                : 28;

            // Calcula duração média do período
            var comPeriodo = registros.Where(r => r.DataFim.HasValue).ToList();
            previsao.DuracaoMediaPeriodo = comPeriodo.Count > 0
                ? (int)Math.Round(comPeriodo.Average(r => (r.DataFim!.Value - r.DataInicio).Days + 1))
                : 5;

            // Previsão da próxima menstruação baseada no último registro
            var ultimo = registros.First(); // já vem ordenado DESC
            var proximaMenstruacao = ultimo.DataInicio.AddDays(previsao.CicloMedio);
            previsao.ProximaMenstruacao = proximaMenstruacao;
            previsao.DiasParaProxima = (proximaMenstruacao - DateTime.UtcNow.Date).Days;

            return previsao;
        }

        private static CicloRegistroDto MapToDto(CicloRegistro registro)
        {
            return new CicloRegistroDto
            {
                Id = registro.Id,
                UsuarioId = registro.UsuarioId,
                DataInicio = registro.DataInicio,
                DataFim = registro.DataFim,
                DuracaoPeriodo = registro.DuracaoPeriodo,
                DuracaoCiclo = registro.DuracaoCiclo,
                Observacoes = registro.Observacoes,
                CreatedAt = registro.CreatedAt
            };
        }
    }
}
