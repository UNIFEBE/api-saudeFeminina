namespace ApiSaudeFeminina.Domain.Entities
{
    public class Usuario : BaseEntity
    {
        public string Nome { get; private set; }
        public string Email { get; private set; }
        public string SenhaHash { get; private set; }
        public DateTime? DataNascimento { get; private set; }
        public string? Telefone { get; private set; }
        public string? Cidade { get; private set; }
        public string? Estado { get; private set; }
        public decimal? Peso { get; private set; }
        public decimal? Altura { get; private set; }
        public string? TipoSanguineo { get; private set; }
        public string? FaseVida { get; private set; }

        protected Usuario() { }

        public Usuario(
            string nome,
            string email,
            string senhaHash,
            DateTime? dataNascimento = null,
            string? telefone = null,
            string? cidade = null,
            string? estado = null)
        {
            SetNome(nome);
            SetEmail(email);
            SetSenhaHash(senhaHash);
            DataNascimento = dataNascimento;
            Telefone = telefone;
            Cidade = cidade;
            SetEstado(estado);
        }

        public void SetNome(string nome)
        {
            if (string.IsNullOrWhiteSpace(nome))
                throw new ArgumentException("O nome não pode ser vazio.");

            if (nome.Length > 150)
                throw new ArgumentException("O nome não pode ter mais de 150 caracteres.");

            Nome = nome;
        }

        public void SetEmail(string email)
        {
            if (string.IsNullOrWhiteSpace(email))
                throw new ArgumentException("O email não pode ser vazio.");

            if (email.Length > 150)
                throw new ArgumentException("O email não pode ter mais de 150 caracteres.");

            if (!email.Contains("@"))
                throw new ArgumentException("Email inválido.");

            Email = email.ToLower();
        }

        public void SetSenhaHash(string senhaHash)
        {
            if (string.IsNullOrWhiteSpace(senhaHash))
                throw new ArgumentException("A senha hash não pode ser vazia.");

            SenhaHash = senhaHash;
        }

        public void SetEstado(string? estado)
        {
            if (!string.IsNullOrWhiteSpace(estado) && estado.Length != 2)
                throw new ArgumentException("O estado deve ter 2 caracteres (UF).");

            Estado = estado?.ToUpper();
        }

        public void Update(
            string nome,
            string email,
            DateTime? dataNascimento,
            string? telefone,
            string? cidade,
            string? estado)
        {
            SetNome(nome);
            SetEmail(email);
            DataNascimento = dataNascimento;
            Telefone = telefone;
            Cidade = cidade;
            SetEstado(estado);
            MarkAsUpdated();
        }

        public void UpdateSenha(string novaSenhaHash)
        {
            SetSenhaHash(novaSenhaHash);
            MarkAsUpdated();
        }

        public void UpdateDadosSaude(
            decimal? peso,
            decimal? altura,
            string? tipoSanguineo,
            string? faseVida)
        {
            if (peso.HasValue && peso.Value < 0)
                throw new ArgumentException("O peso não pode ser negativo.");

            if (altura.HasValue && altura.Value < 0)
                throw new ArgumentException("A altura não pode ser negativa.");

            if (!string.IsNullOrWhiteSpace(tipoSanguineo) && tipoSanguineo.Length > 3)
                throw new ArgumentException("O tipo sanguíneo deve ter no máximo 3 caracteres.");

            if (!string.IsNullOrWhiteSpace(faseVida) && faseVida.Length > 50)
                throw new ArgumentException("A fase da vida deve ter no máximo 50 caracteres.");

            Peso = peso;
            Altura = altura;
            TipoSanguineo = tipoSanguineo?.ToUpper();
            FaseVida = faseVida;
            MarkAsUpdated();
        }
    }
}
