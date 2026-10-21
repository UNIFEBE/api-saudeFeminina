using ApiSaudeFeminina.Application.DTOs;
using ApiSaudeFeminina.Application.Interfaces;
using ApiSaudeFeminina.Domain.Entities;
using ApiSaudeFeminina.Domain.Interfaces;

namespace ApiSaudeFeminina.Application.Services
{
    public class ConsultaService : IConsultaService
    {
        private readonly IConsultaRepository _repository;

        public ConsultaService(IConsultaRepository repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<ConsultaDto>> GetByUsuarioIdAsync(Guid usuarioId)
        {
            var consultas = await _repository.GetByUsuarioIdAsync(usuarioId);
            return consultas
                .OrderBy(c => c.DataConsulta)
                .Select(MapToDto);
        }

        public async Task<ConsultaDto?> GetByIdAsync(Guid id)
        {
            var consulta = await _repository.GetByIdAsync(id);
            return consulta is null ? null : MapToDto(consulta);
        }

        public async Task<ConsultaDto> CreateAsync(CreateConsultaDto dto)
        {
            var consulta = new Consulta(
                dto.UsuarioId,
                dto.Especialidade,
                dto.DataConsulta,
                string.IsNullOrWhiteSpace(dto.Status) ? "Confirmada" : dto.Status);

            await _repository.AddAsync(consulta);
            await _repository.SaveChangesAsync();

            return MapToDto(consulta);
        }

        public async Task<ConsultaDto?> UpdateStatusAsync(Guid id, UpdateConsultaStatusDto dto)
        {
            var consulta = await _repository.GetByIdAsync(id);
            if (consulta is null) return null;

            consulta.Update(consulta.Especialidade, consulta.DataConsulta, dto.Status);
            _repository.Update(consulta);
            await _repository.SaveChangesAsync();

            return MapToDto(consulta);
        }

        public async Task<bool> DeleteAsync(Guid id)
        {
            var consulta = await _repository.GetByIdAsync(id);
            if (consulta is null) return false;

            _repository.Remove(consulta);
            await _repository.SaveChangesAsync();
            return true;
        }

        private static ConsultaDto MapToDto(Consulta c) => new()
        {
            Id           = c.Id,
            UsuarioId    = c.UsuarioId,
            Especialidade = c.Especialidade,
            DataConsulta = c.DataConsulta,
            Status       = c.Status,
            CreatedAt    = c.CreatedAt,
        };
    }
}
