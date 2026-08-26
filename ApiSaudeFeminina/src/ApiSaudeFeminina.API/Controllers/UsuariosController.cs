using ApiSaudeFeminina.Application.DTOs;
using ApiSaudeFeminina.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace ApiSaudeFeminina.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UsuariosController : ControllerBase
    {
        private readonly IUsuarioService _usuarioService;

        public UsuariosController(IUsuarioService usuarioService)
        {
            _usuarioService = usuarioService;
        }

        /// <summary>
        /// Retorna todos os usuários
        /// </summary>
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var usuarios = await _usuarioService.GetAllAsync();
            return Ok(usuarios);
        }

        /// <summary>
        /// Retorna um usuário específico por ID
        /// </summary>
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var usuario = await _usuarioService.GetByIdAsync(id);
            if (usuario is null) 
                return NotFound(new { message = "Usuário não encontrado." });

            return Ok(usuario);
        }

        /// <summary>
        /// Retorna um usuário por email
        /// </summary>
        [HttpGet("email/{email}")]
        public async Task<IActionResult> GetByEmail(string email)
        {
            var usuario = await _usuarioService.GetByEmailAsync(email);
            if (usuario is null) 
                return NotFound(new { message = "Usuário não encontrado." });

            return Ok(usuario);
        }

        /// <summary>
        /// Cria um novo usuário
        /// </summary>
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateUsuarioDto dto)
        {
            try
            {
                var created = await _usuarioService.CreateAsync(dto);
                return Created($"/api/usuarios/{created.Id}", created);
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
            catch (ArgumentException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        /// <summary>
        /// Atualiza os dados básicos de um usuário
        /// </summary>
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(Guid id, [FromBody] UpdateUsuarioDto dto)
        {
            try
            {
                var updated = await _usuarioService.UpdateAsync(id, dto);
                if (updated is null) 
                    return NotFound(new { message = "Usuário não encontrado." });

                return Ok(updated);
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
            catch (ArgumentException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        /// <summary>
        /// Atualiza a senha do usuário
        /// </summary>
        [HttpPut("{id}/senha")]
        public async Task<IActionResult> UpdateSenha(Guid id, [FromBody] UpdateSenhaDto dto)
        {
            try
            {
                var result = await _usuarioService.UpdateSenhaAsync(id, dto);
                if (!result) 
                    return NotFound(new { message = "Usuário não encontrado." });

                return Ok(new { message = "Senha atualizada com sucesso." });
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        /// <summary>
        /// Atualiza os dados de saúde do usuário
        /// </summary>
        [HttpPut("{id}/dados-saude")]
        public async Task<IActionResult> UpdateDadosSaude(Guid id, [FromBody] UpdateDadosSaudeDto dto)
        {
            try
            {
                var updated = await _usuarioService.UpdateDadosSaudeAsync(id, dto);
                if (updated is null) 
                    return NotFound(new { message = "Usuário não encontrado." });

                return Ok(updated);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        /// <summary>
        /// Deleta um usuário
        /// </summary>
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var result = await _usuarioService.DeleteAsync(id);
            if (!result) 
                return NotFound(new { message = "Usuário não encontrado." });

            return NoContent();
        }
    }
}
