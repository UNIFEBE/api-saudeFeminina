namespace ApiSaudeFeminina.Domain.Entities
{
    /// <summary>
    /// Representa um período menstrual registrado (início e fim).
    /// Permite múltiplos registros por usuário para formar o histórico.
    /// </summary>
    public class CicloRegistro : BaseEntity
    {
        public Guid UsuarioId { get; private set; }
        public DateTime DataInicio { get; private set; }
        public DateTime? DataFim { get; private set; }
        public int? DuracaoCiclo { get; private set; }
        public string? Observacoes { get; private set; }

        // Navegação
        public Usuario? Usuario { get; private set; }

        protected CicloRegistro() { }

        public CicloRegistro(Guid usuarioId, DateTime dataInicio, DateTime? dataFim, string? observacoes)
        {
            UsuarioId = usuarioId;
            SetDataInicio(dataInicio);
            if (dataFim.HasValue)
                SetDataFim(dataFim.Value);
            Observacoes = observacoes;
        }

        public void FinalizarPeriodo(DateTime dataFim)
        {
            SetDataFim(dataFim);
            MarkAsUpdated();
        }

        public void Update(DateTime dataInicio, DateTime? dataFim, string? observacoes)
        {
            SetDataInicio(dataInicio);
            DataFim = null;
            if (dataFim.HasValue)
                SetDataFim(dataFim.Value);
            Observacoes = observacoes;
            MarkAsUpdated();
        }

        public void SetDuracaoCiclo(int duracao)
        {
            if (duracao < 1)
                throw new ArgumentException("A duração do ciclo deve ser positiva.");
            DuracaoCiclo = duracao;
            MarkAsUpdated();
        }

        /// <summary>
        /// Duração do período menstrual em dias (DataFim - DataInicio + 1)
        /// </summary>
        public int? DuracaoPeriodo => DataFim.HasValue
            ? (DataFim.Value - DataInicio).Days + 1
            : null;

        private void SetDataInicio(DateTime data)
        {
            if (data > DateTime.UtcNow.Date.AddDays(1))
                throw new ArgumentException("A data de início não pode ser no futuro.");
            DataInicio = data;
        }

        private void SetDataFim(DateTime data)
        {
            if (data < DataInicio)
                throw new ArgumentException("A data de fim não pode ser anterior à data de início.");
            DataFim = data;
        }
    }
}
