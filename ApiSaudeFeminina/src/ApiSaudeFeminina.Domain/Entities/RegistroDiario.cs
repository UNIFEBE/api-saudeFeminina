namespace ApiSaudeFeminina.Domain.Entities
{
    public class RegistroDiario : BaseEntity
    {
        public Guid UsuarioId { get; private set; }
        public DateTime DataRegistro { get; private set; }
        public string? Humor { get; private set; }
        public string Fluxo { get; private set; } = "Nenhum";
        public decimal? Temperatura { get; private set; }
        public string[]? Sintomas { get; private set; }
        public string? Observacoes { get; private set; }

        // Navegação
        public Usuario? Usuario { get; private set; }

        protected RegistroDiario() { }

        public RegistroDiario(Guid usuarioId, DateTime dataRegistro, string? humor, string fluxo, decimal? temperatura, string[]? sintomas, string? observacoes)
        {
            UsuarioId = usuarioId;
            DataRegistro = dataRegistro.Date;
            Humor = humor;
            SetFluxo(fluxo);
            Temperatura = temperatura;
            Sintomas = sintomas;
            Observacoes = observacoes;
        }

        public void Update(string? humor, string fluxo, decimal? temperatura, string[]? sintomas, string? observacoes)
        {
            Humor = humor;
            SetFluxo(fluxo);
            Temperatura = temperatura;
            Sintomas = sintomas;
            Observacoes = observacoes;
            MarkAsUpdated();
        }

        private void SetFluxo(string fluxo)
        {
            var validos = new[] { "Nenhum", "Leve", "Moderado", "Intenso" };
            if (!validos.Contains(fluxo))
                throw new ArgumentException("Fluxo deve ser: Nenhum, Leve, Moderado ou Intenso.");
            Fluxo = fluxo;
        }
    }
}
