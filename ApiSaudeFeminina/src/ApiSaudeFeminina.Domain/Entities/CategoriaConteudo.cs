namespace ApiSaudeFeminina.Domain.Entities
{
    public class CategoriaConteudo : BaseEntity
    {
        public string Nome { get; private set; }
        public string? Icone { get; private set; }

        // Navegação
        public ICollection<Artigo> Artigos { get; private set; } = new List<Artigo>();

        protected CategoriaConteudo() { }

        public CategoriaConteudo(string nome, string? icone = null)
        {
            SetNome(nome);
            Icone = icone;
        }

        public void SetNome(string nome)
        {
            if (string.IsNullOrWhiteSpace(nome))
                throw new ArgumentException("O nome da categoria não pode ser vazio.");

            if (nome.Length > 50)
                throw new ArgumentException("O nome da categoria não pode ter mais de 50 caracteres.");

            Nome = nome;
        }

        public void Update(string nome, string? icone)
        {
            SetNome(nome);
            Icone = icone;
            MarkAsUpdated();
        }
    }
}
