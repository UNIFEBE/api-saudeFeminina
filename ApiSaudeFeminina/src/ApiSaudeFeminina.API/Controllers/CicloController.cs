using ApiSaudeFeminina.Application.DTOs;
using ApiSaudeFeminina.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace ApiSaudeFeminina.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CicloController : ControllerBase
    {
        private readonly ICicloService _cicloService;

        public CicloController(ICicloService cicloService)
        {
            _cicloService = cicloService;
        }

        // ── Configuração do Ciclo ──────────────────────────────────────

        /// <summary>
        /// Retorna a configuração do ciclo de um usuário
        /// </summary>
        [HttpGet("config/{usuarioId}")]
        public async Task<IActionResult> GetConfig(Guid usuarioId)
        {
            var config = await _cicloService.GetConfigByUsuarioIdAsync(usuarioId);
            if (config is null)
                return NotFound(new { message = "Configuração de ciclo não encontrada para este usuário." });

            return Ok(config);
        }

        /// <summary>
        /// Cria ou atualiza a configuração do ciclo (upsert)
        /// </summary>
        [HttpPost("config")]
        public async Task<IActionResult> CreateOrUpdateConfig([FromBody] CreateCicloConfigDto dto)
        {
            try
            {
                var config = await _cicloService.CreateOrUpdateConfigAsync(dto);
                return Ok(config);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        /// <summary>
        /// Atualiza a configuração do ciclo de um usuário
        /// </summary>
        [HttpPut("config/{usuarioId}")]
        public async Task<IActionResult> UpdateConfig(Guid usuarioId, [FromBody] UpdateCicloConfigDto dto)
        {
            try
            {
                var config = await _cicloService.UpdateConfigAsync(usuarioId, dto);
                if (config is null)
                    return NotFound(new { message = "Configuração de ciclo não encontrada." });

                return Ok(config);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        // ── Registros Diários ──────────────────────────────────────────

        /// <summary>
        /// Retorna todos os registros diários de um usuário
        /// </summary>
        [HttpGet("registros/{usuarioId}")]
        public async Task<IActionResult> GetRegistros(Guid usuarioId)
        {
            var registros = await _cicloService.GetRegistrosByUsuarioIdAsync(usuarioId);
            return Ok(registros);
        }

        /// <summary>
        /// Retorna registros diários de um período específico
        /// </summary>
        [HttpGet("registros/{usuarioId}/periodo")]
        public async Task<IActionResult> GetRegistrosByPeriodo(Guid usuarioId, [FromQuery] DateTime dataInicio, [FromQuery] DateTime dataFim)
        {
            var registros = await _cicloService.GetRegistrosByPeriodoAsync(usuarioId, dataInicio, dataFim);
            return Ok(registros);
        }

        /// <summary>
        /// Retorna o registro de uma data específica
        /// </summary>
        [HttpGet("registros/{usuarioId}/data/{data}")]
        public async Task<IActionResult> GetRegistroByData(Guid usuarioId, DateTime data)
        {
            var registro = await _cicloService.GetRegistroByDataAsync(usuarioId, data);
            if (registro is null)
                return NotFound(new { message = "Nenhum registro encontrado para esta data." });

            return Ok(registro);
        }

        /// <summary>
        /// Cria ou atualiza um registro diário (upsert por usuário + data)
        /// </summary>
        [HttpPost("registros")]
        public async Task<IActionResult> CreateOrUpdateRegistro([FromBody] CreateRegistroDiarioDto dto)
        {
            try
            {
                var registro = await _cicloService.CreateOrUpdateRegistroAsync(dto);
                return Ok(registro);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        /// <summary>
        /// Deleta um registro diário
        /// </summary>
        [HttpDelete("registros/{id}")]
        public async Task<IActionResult> DeleteRegistro(Guid id)
        {
            var result = await _cicloService.DeleteRegistroAsync(id);
            if (!result)
                return NotFound(new { message = "Registro não encontrado." });

            return NoContent();
        }
    }
}
