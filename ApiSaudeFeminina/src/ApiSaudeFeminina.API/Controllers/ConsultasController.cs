using ApiSaudeFeminina.Application.DTOs;
using ApiSaudeFeminina.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace ApiSaudeFeminina.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ConsultasController : ControllerBase
    {
        private readonly IConsultaService _service;

        public ConsultasController(IConsultaService service)
        {
            _service = service;
        }

        /// <summary>Lista todas as consultas de um usuário</summary>
        [HttpGet("usuario/{usuarioId}")]
        public async Task<IActionResult> GetByUsuario(Guid usuarioId)
        {
            var consultas = await _service.GetByUsuarioIdAsync(usuarioId);
            return Ok(consultas);
        }

        /// <summary>Retorna uma consulta pelo ID</summary>
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var consulta = await _service.GetByIdAsync(id);
            if (consulta is null)
                return NotFound(new { message = "Consulta não encontrada." });
            return Ok(consulta);
        }

        /// <summary>Cria uma nova consulta</summary>
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateConsultaDto dto)
        {
            try
            {
                var created = await _service.CreateAsync(dto);
                return Created($"/api/consultas/{created.Id}", created);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        /// <summary>Atualiza o status de uma consulta</summary>
        [HttpPatch("{id}/status")]
        public async Task<IActionResult> UpdateStatus(Guid id, [FromBody] UpdateConsultaStatusDto dto)
        {
            try
            {
                var updated = await _service.UpdateStatusAsync(id, dto);
                if (updated is null)
                    return NotFound(new { message = "Consulta não encontrada." });
                return Ok(updated);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        /// <summary>Cancela / deleta uma consulta</summary>
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var result = await _service.DeleteAsync(id);
            if (!result)
                return NotFound(new { message = "Consulta não encontrada." });
            return NoContent();
        }
    }
}
