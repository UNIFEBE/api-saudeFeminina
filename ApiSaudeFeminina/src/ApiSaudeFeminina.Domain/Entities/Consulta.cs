namespace ApiSaudeFeminina.Domain.Entities
{
    public class Consulta : BaseEntity
    {
        public Guid UsuarioId { get; private set; }

        /// <summary>Ginecologia | Mastologia | Clínico | Obstetrícia</summary>
        public string Especialidade { get; private set; } = string.Empty;

        public DateTime DataConsulta { get; private set; }

        /// <summary>Confirmada | Cancelada | Concluída</summary>
        public string Status { get; private set; } = "Confirmada";

        private static readonly string[] EspecialidadesValidas =
            { "Ginecologia", "Mastologia", "Clínico", "Obstetrícia" };

        private static readonly string[] StatusValidos =
            { "Confirmada", "Cancelada", "Concluída" };

        protected Consulta() { }

        public Consulta(Guid usuarioId, string especialidade, DateTime dataConsulta, string status = "Confirmada")
        {
            UsuarioId = usuarioId;
            SetEspecialidade(especialidade);
            DataConsulta = dataConsulta;
            SetStatus(status);
        }

        public void Update(string especialidade, DateTime dataConsulta, string status)
        {
            SetEspecialidade(especialidade);
            DataConsulta = dataConsulta;
            SetStatus(status);
            MarkAsUpdated();
        }

        private void SetEspecialidade(string especialidade)
        {
            if (!EspecialidadesValidas.Contains(especialidade))
                throw new ArgumentException($"Especialidade inválida. Valores aceitos: {string.Join(", ", EspecialidadesValidas)}");
            Especialidade = especialidade;
        }

        private void SetStatus(string status)
        {
            if (!StatusValidos.Contains(status))
                throw new ArgumentException($"Status inválido. Valores aceitos: {string.Join(", ", StatusValidos)}");
            Status = status;
        }
    }
}
