namespace ApiSaudeFeminina.Application.DTOs
{
    public class RegistroDiarioDto
    {
        public Guid Id { get; set; }
        public Guid UsuarioId { get; set; }
        public DateTime DataRegistro { get; set; }
        public string? Humor { get; set; }
        public string Fluxo { get; set; } = "Nenhum";
        public decimal? Temperatura { get; set; }
        public string[]? Sintomas { get; set; }
        public string? Observacoes { get; set; }
        public DateTime CreatedAt { get; set; }
    }

    public class CreateRegistroDiarioDto
    {
        public Guid UsuarioId { get; set; }
        public DateTime DataRegistro { get; set; }
        public string? Humor { get; set; }
        public string Fluxo { get; set; } = "Nenhum";
        public decimal? Temperatura { get; set; }
        public string[]? Sintomas { get; set; }
        public string? Observacoes { get; set; }
    }

    public class UpdateRegistroDiarioDto
    {
        public string? Humor { get; set; }
        public string Fluxo { get; set; } = "Nenhum";
        public decimal? Temperatura { get; set; }
        public string[]? Sintomas { get; set; }
        public string? Observacoes { get; set; }
    }
}
