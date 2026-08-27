namespace ApiSaudeFeminina.Application.DTOs
{
    public class CicloRegistroDto
    {
        public Guid Id { get; set; }
        public Guid UsuarioId { get; set; }
        public DateTime DataInicio { get; set; }
        public DateTime? DataFim { get; set; }
        public int? DuracaoPeriodo { get; set; }
        public int? DuracaoCiclo { get; set; }
        public string? Observacoes { get; set; }
        public DateTime CreatedAt { get; set; }
    }

    public class CreateCicloRegistroDto
    {
        public Guid UsuarioId { get; set; }
        public DateTime DataInicio { get; set; }
        public DateTime? DataFim { get; set; }
        public string? Observacoes { get; set; }
    }

    public class UpdateCicloRegistroDto
    {
        public DateTime DataInicio { get; set; }
        public DateTime? DataFim { get; set; }
        public string? Observacoes { get; set; }
    }

    /// <summary>
    /// Retorno com previsão calculada a partir do histórico
    /// </summary>
    public class PrevisaoCicloDto
    {
        public DateTime? ProximaMenstruacao { get; set; }
        public int DiasParaProxima { get; set; }
        public int CicloMedio { get; set; }
        public int DuracaoMediaPeriodo { get; set; }
        public int TotalRegistros { get; set; }
        public List<CicloRegistroDto> UltimosRegistros { get; set; } = new();
    }
}
