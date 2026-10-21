namespace ApiSaudeFeminina.Application.DTOs
{
    public class ConsultaDto
    {
        public Guid Id { get; set; }
        public Guid UsuarioId { get; set; }
        public string Especialidade { get; set; } = string.Empty;
        public DateTime DataConsulta { get; set; }
        public string Status { get; set; } = "Confirmada";
        public DateTime CreatedAt { get; set; }
    }

    public class CreateConsultaDto
    {
        public Guid UsuarioId { get; set; }
        public string Especialidade { get; set; } = string.Empty;
        public DateTime DataConsulta { get; set; }
        public string Status { get; set; } = "Confirmada";
    }

    public class UpdateConsultaStatusDto
    {
        /// <summary>Confirmada | Cancelada | Concluída</summary>
        public string Status { get; set; } = string.Empty;
    }
}
