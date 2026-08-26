namespace ApiSaudeFeminina.Domain.Entities
{
    public class Artigo : BaseEntity
    {
        public Guid CategoriaId { get; private set; }
        public string Titulo { get; private set; }
        public string Resumo { get; private set; }
        public string? Conteudo { get; private set; }
        public bool Publicado { get; private set; }

        // Navegação
        public CategoriaConteudo Categoria { get; private set; } = null!;

        protected Artigo() { }

        public Artigo(
            Guid categoriaId,
            string titulo,
            string resumo,
            string? conteudo = null,
            bool publicado = true)
        {
            CategoriaId = categoriaId;
            SetTitulo(titulo);
            SetResumo(resumo);
            Conteudo = conteudo;
            Publicado = publicado;
        }

        public void SetTitulo(string titulo)
        {
            if (string.IsNullOrWhiteSpace(titulo))
                throw new ArgumentException("O título não pode ser vazio.");

            if (titulo.Length > 255)
                throw new ArgumentException("O título não pode ter mais de 255 caracteres.");

            Titulo = titulo;
        }

        public void SetResumo(string resumo)
        {
            if (string.IsNullOrWhiteSpace(resumo))
                throw new ArgumentException("O resumo não pode ser vazio.");

            Resumo = resumo;
        }

        public void Update(
            Guid categoriaId,
            string titulo,
            string resumo,
            string? conteudo,
            bool publicado)
        {
            CategoriaId = categoriaId;
            SetTitulo(titulo);
            SetResumo(resumo);
            Conteudo = conteudo;
            Publicado = publicado;
            MarkAsUpdated();
        }

        public void TogglePublicado()
        {
            Publicado = !Publicado;
            MarkAsUpdated();
        }
    }
}
