using Microsoft.AspNetCore.Mvc;

namespace ApiSaudeFeminina.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UploadController : ControllerBase
    {
        private readonly IWebHostEnvironment _env;

        public UploadController(IWebHostEnvironment env)
        {
            _env = env;
        }

        /// <summary>
        /// Faz upload de uma imagem e retorna a URL
        /// </summary>
        [HttpPost("imagem")]
        public async Task<IActionResult> UploadImagem(IFormFile arquivo)
        {
            if (arquivo is null || arquivo.Length == 0)
                return BadRequest(new { message = "Nenhum arquivo enviado." });

            var extensoesPermitidas = new[] { ".jpg", ".jpeg", ".png", ".gif", ".webp" };
            var extensao = Path.GetExtension(arquivo.FileName).ToLower();

            if (!extensoesPermitidas.Contains(extensao))
                return BadRequest(new { message = "Formato de arquivo não permitido. Use: jpg, png, gif ou webp." });

            if (arquivo.Length > 5 * 1024 * 1024)
                return BadRequest(new { message = "Arquivo muito grande. Máximo: 5MB." });

            var uploadsPath = Path.Combine(_env.ContentRootPath, "wwwroot", "uploads");
            Directory.CreateDirectory(uploadsPath);

            var nomeArquivo = $"{Guid.NewGuid()}{extensao}";
            var caminhoCompleto = Path.Combine(uploadsPath, nomeArquivo);

            using (var stream = new FileStream(caminhoCompleto, FileMode.Create))
            {
                await arquivo.CopyToAsync(stream);
            }

            var url = $"{Request.Scheme}://{Request.Host}/uploads/{nomeArquivo}";

            return Ok(new { url });
        }
    }
}
