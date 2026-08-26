using ApiSaudeFeminina.Application.DTOs;
using ApiSaudeFeminina.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace ApiSaudeFeminina.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ArtigosController : ControllerBase
    {
        private readonly IArtigoService _artigoService;

        public ArtigosController(IArtigoService artigoService)
        {
            _artigoService = artigoService;
        }

        /// <summary>
        /// Retorna todos os artigos
        /// </summary>
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var artigos = await _artigoService.GetAllAsync();
            return Ok(artigos);
        }

        /// <summary>
        /// Retorna apenas artigos publicados
        /// </summary>
        [HttpGet("publicados")]
        public async Task<IActionResult> GetPublicados()
        {
            var artigos = await _artigoService.GetPublicadosAsync();
            return Ok(artigos);
        }

        /// <summary>
        /// Retorna artigos por categoria
        /// </summary>
        [HttpGet("categoria/{categoriaId}")]
        public async Task<IActionResult> GetByCategoria(Guid categoriaId)
        {
            var artigos = await _artigoService.GetByCategoriaIdAsync(categoriaId);
            return Ok(artigos);
        }

        /// <summary>
        /// Retorna todas as categorias disponíveis
        /// </summary>
        [HttpGet("categorias")]
        public async Task<IActionResult> GetCategorias()
        {
            var categorias = await _artigoService.GetCategoriasAsync();
            return Ok(categorias);
        }

        /// <summary>
        /// Retorna um artigo por ID
        /// </summary>
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var artigo = await _artigoService.GetByIdAsync(id);
            if (artigo is null)
                return NotFound(new { message = "Artigo não encontrado." });

            return Ok(artigo);
        }

        /// <summary>
        /// Cria um novo artigo
        /// </summary>
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateArtigoDto dto)
        {
            try
            {
                var created = await _artigoService.CreateAsync(dto);
                return Created($"/api/artigos/{created.Id}", created);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        /// <summary>
        /// Atualiza um artigo existente
        /// </summary>
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(Guid id, [FromBody] UpdateArtigoDto dto)
        {
            try
            {
                var updated = await _artigoService.UpdateAsync(id, dto);
                if (updated is null)
                    return NotFound(new { message = "Artigo não encontrado." });

                return Ok(updated);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        /// <summary>
        /// Deleta um artigo
        /// </summary>
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var result = await _artigoService.DeleteAsync(id);
            if (!result)
                return NotFound(new { message = "Artigo não encontrado." });

            return NoContent();
        }
    }
}
