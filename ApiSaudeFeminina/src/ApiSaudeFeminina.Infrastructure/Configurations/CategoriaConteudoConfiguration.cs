using ApiSaudeFeminina.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ApiSaudeFeminina.Infrastructure.Configurations
{
    public class CategoriaConteudoConfiguration : IEntityTypeConfiguration<CategoriaConteudo>
    {
        public void Configure(EntityTypeBuilder<CategoriaConteudo> builder)
        {
            builder.ToTable("categoria_conteudo");

            builder.HasKey(c => c.Id);

            builder.Property(c => c.Id)
                .HasColumnName("id")
                .ValueGeneratedOnAdd();

            builder.Property(c => c.Nome)
                .HasColumnName("nome")
                .HasMaxLength(50)
                .IsRequired();

            builder.HasIndex(c => c.Nome)
                .IsUnique();

            builder.Property(c => c.Icone)
                .HasColumnName("icone")
                .HasMaxLength(50);

            // categoria_conteudo não tem colunas criado_em / atualizado_em no banco
            builder.Ignore(c => c.CreatedAt);
            builder.Ignore(c => c.UpdatedAt);
        }
    }
}
