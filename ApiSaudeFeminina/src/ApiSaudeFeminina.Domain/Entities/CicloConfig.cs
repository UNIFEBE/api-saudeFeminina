namespace ApiSaudeFeminina.Domain.Entities
{
    public class CicloConfig : BaseEntity
    {
        public Guid UsuarioId { get; private set; }
        public DateTime DataUltimaMenstruacao { get; private set; }
        public int DuracaoCiclo { get; private set; }
        public int DuracaoMenstruacao { get; private set; }
        public bool CicloRegular { get; private set; }

        // Navegação
        public Usuario? Usuario { get; private set; }

        protected CicloConfig() { }

        public CicloConfig(Guid usuarioId, DateTime dataUltimaMenstruacao, int duracaoCiclo, int duracaoMenstruacao, bool cicloRegular)
        {
            UsuarioId = usuarioId;
            SetDataUltimaMenstruacao(dataUltimaMenstruacao);
            SetDuracaoCiclo(duracaoCiclo);
            SetDuracaoMenstruacao(duracaoMenstruacao);
            CicloRegular = cicloRegular;
        }

        public void Update(DateTime dataUltimaMenstruacao, int duracaoCiclo, int duracaoMenstruacao, bool cicloRegular)
        {
            SetDataUltimaMenstruacao(dataUltimaMenstruacao);
            SetDuracaoCiclo(duracaoCiclo);
            SetDuracaoMenstruacao(duracaoMenstruacao);
            CicloRegular = cicloRegular;
            MarkAsUpdated();
        }

        private void SetDataUltimaMenstruacao(DateTime data)
        {
            if (data > DateTime.UtcNow.Date.AddDays(1))
                throw new ArgumentException("A data da última menstruação não pode ser no futuro.");
            DataUltimaMenstruacao = data;
        }

        private void SetDuracaoCiclo(int duracao)
        {
            if (duracao < 15 || duracao > 60)
                throw new ArgumentException("A duração do ciclo deve ser entre 15 e 60 dias.");
            DuracaoCiclo = duracao;
        }

        private void SetDuracaoMenstruacao(int duracao)
        {
            if (duracao < 1 || duracao > 15)
                throw new ArgumentException("A duração da menstruação deve ser entre 1 e 15 dias.");
            DuracaoMenstruacao = duracao;
        }

        /// <summary>
        /// Calcula a data prevista para o próximo ciclo
        /// </summary>
        public DateTime CalcularProximoCiclo()
        {
            return DataUltimaMenstruacao.AddDays(DuracaoCiclo);
        }

        /// <summary>
        /// Calcula o início da janela fértil (aproximadamente 14 dias antes do próximo ciclo, durando 6 dias)
        /// </summary>
        public DateTime CalcularInicioJanelaFertil()
        {
            return CalcularProximoCiclo().AddDays(-14);
        }
    }
}
