using ApiSaudeFeminina.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ApiSaudeFeminina.Infrastructure.Configurations
{
    public class ArtigoConfiguration : IEntityTypeConfiguration<Artigo>
    {
        public void Configure(EntityTypeBuilder<Artigo> builder)
        {
            builder.ToTable("artigo");

            builder.HasKey(a => a.Id);

            builder.Property(a => a.Id)
                .HasColumnName("id")
                .ValueGeneratedOnAdd();

            builder.Property(a => a.CategoriaId)
                .HasColumnName("categoria_id")
                .IsRequired();

            builder.Property(a => a.Titulo)
                .HasColumnName("titulo")
                .HasMaxLength(255)
                .IsRequired();

            builder.Property(a => a.Resumo)
                .HasColumnName("resumo")
                .HasColumnType("text")
                .IsRequired();

            builder.Property(a => a.Conteudo)
                .HasColumnName("conteudo")
                .HasColumnType("text");

            builder.Property(a => a.Publicado)
                .HasColumnName("publicado")
                .HasDefaultValue(true);

            builder.Property(a => a.CreatedAt)
                .HasColumnName("criado_em")
                .HasColumnType("timestamp with time zone")
                .IsRequired();

            builder.Ignore(a => a.UpdatedAt);

            builder.HasOne(a => a.Categoria)
                .WithMany(c => c.Artigos)
                .HasForeignKey(a => a.CategoriaId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasIndex(a => a.CategoriaId);
        }
    }
}
