using ApiSaudeFeminina.Application.DTOs;
using ApiSaudeFeminina.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace ApiSaudeFeminina.API.Controllers
{
    [ApiController]
    [Route("api/ciclo-registros")]
    public class CicloRegistroController : ControllerBase
    {
        private readonly ICicloRegistroService _service;

        public CicloRegistroController(ICicloRegistroService service)
        {
            _service = service;
        }

        /// <summary>
        /// Retorna todos os registros de períodos menstruais de um usuário (histórico)
        /// </summary>
        [HttpGet("{usuarioId}")]
        public async Task<IActionResult> GetAll(Guid usuarioId)
        {
            var registros = await _service.GetAllByUsuarioAsync(usuarioId);
            return Ok(registros);
        }

        /// <summary>
        /// Retorna a previsão do próximo ciclo baseada no histórico
        /// </summary>
        [HttpGet("{usuarioId}/previsao")]
        public async Task<IActionResult> GetPrevisao(Guid usuarioId)
        {
            var previsao = await _service.GetPrevisaoAsync(usuarioId);
            return Ok(previsao);
        }

        /// <summary>
        /// Retorna um registro específico por ID
        /// </summary>
        [HttpGet("detalhe/{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var registro = await _service.GetByIdAsync(id);
            if (registro is null)
                return NotFound(new { message = "Registro não encontrado." });
            return Ok(registro);
        }

        /// <summary>
        /// Registra um novo período menstrual (início e opcionalmente fim)
        /// </summary>
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateCicloRegistroDto dto)
        {
            try
            {
                var registro = await _service.CreateAsync(dto);
                return Created($"/api/ciclo-registros/detalhe/{registro.Id}", registro);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        /// <summary>
        /// Atualiza um registro de período (datas e observações)
        /// </summary>
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(Guid id, [FromBody] UpdateCicloRegistroDto dto)
        {
            try
            {
                var registro = await _service.UpdateAsync(id, dto);
                if (registro is null)
                    return NotFound(new { message = "Registro não encontrado." });
                return Ok(registro);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        /// <summary>
        /// Finaliza um período em andamento (registra a data de término)
        /// </summary>
        [HttpPatch("{id}/finalizar")]
        public async Task<IActionResult> Finalizar(Guid id, [FromBody] FinalizarPeriodoDto dto)
        {
            try
            {
                var registro = await _service.FinalizarPeriodoAsync(id, dto.DataFim);
                if (registro is null)
                    return NotFound(new { message = "Registro não encontrado." });
                return Ok(registro);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        /// <summary>
        /// Deleta um registro de período
        /// </summary>
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var result = await _service.DeleteAsync(id);
            if (!result)
                return NotFound(new { message = "Registro não encontrado." });
            return NoContent();
        }
    }

    public class FinalizarPeriodoDto
    {
        public DateTime DataFim { get; set; }
    }
}
