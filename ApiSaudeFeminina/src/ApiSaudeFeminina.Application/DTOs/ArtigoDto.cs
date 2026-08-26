namespace ApiSaudeFeminina.Application.DTOs
{
    public class ArtigoDto
    {
        public Guid Id { get; set; }
        public Guid CategoriaId { get; set; }
        public string CategoriaNome { get; set; } = string.Empty;
        public string Titulo { get; set; } = string.Empty;
        public string Resumo { get; set; } = string.Empty;
        public string? Conteudo { get; set; }
        public bool Publicado { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
    }

    public class CreateArtigoDto
    {
        public Guid CategoriaId { get; set; }
        public string Titulo { get; set; } = string.Empty;
        public string Resumo { get; set; } = string.Empty;
        public string? Conteudo { get; set; }
        public bool Publicado { get; set; } = true;
    }

    public class UpdateArtigoDto
    {
        public Guid CategoriaId { get; set; }
        public string Titulo { get; set; } = string.Empty;
        public string Resumo { get; set; } = string.Empty;
        public string? Conteudo { get; set; }
        public bool Publicado { get; set; }
    }

    public class CategoriaConteudoDto
    {
        public Guid Id { get; set; }
        public string Nome { get; set; } = string.Empty;
        public string? Icone { get; set; }
    }
}
