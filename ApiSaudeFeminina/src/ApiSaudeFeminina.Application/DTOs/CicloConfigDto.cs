namespace ApiSaudeFeminina.Application.DTOs
{
    public class CicloConfigDto
    {
        public Guid Id { get; set; }
        public Guid UsuarioId { get; set; }
        public DateTime DataUltimaMenstruacao { get; set; }
        public int DuracaoCiclo { get; set; }
        public int DuracaoMenstruacao { get; set; }
        public bool CicloRegular { get; set; }
        public DateTime? UpdatedAt { get; set; }

        // Campos calculados
        public DateTime ProximoCiclo { get; set; }
        public DateTime InicioJanelaFertil { get; set; }
        public int DiasParaProximoCiclo { get; set; }
    }

    public class CreateCicloConfigDto
    {
        public Guid UsuarioId { get; set; }
        public DateTime DataUltimaMenstruacao { get; set; }
        public int DuracaoCiclo { get; set; } = 28;
        public int DuracaoMenstruacao { get; set; } = 5;
        public bool CicloRegular { get; set; } = true;
    }

    public class UpdateCicloConfigDto
    {
        public DateTime DataUltimaMenstruacao { get; set; }
        public int DuracaoCiclo { get; set; }
        public int DuracaoMenstruacao { get; set; }
        public bool CicloRegular { get; set; }
    }
}
