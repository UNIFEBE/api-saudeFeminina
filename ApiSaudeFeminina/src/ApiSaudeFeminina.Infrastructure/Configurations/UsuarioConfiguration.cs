using ApiSaudeFeminina.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ApiSaudeFeminina.Infrastructure.Configurations
{
    public class UsuarioConfiguration : IEntityTypeConfiguration<Usuario>
    {
        public void Configure(EntityTypeBuilder<Usuario> builder)
        {
            builder.ToTable("usuario");

            builder.HasKey(u => u.Id);

            builder.Property(u => u.Id)
                .HasColumnName("id")
                .ValueGeneratedOnAdd();

            builder.Property(u => u.Nome)
                .HasColumnName("nome")
                .HasMaxLength(150)
                .IsRequired();

            builder.Property(u => u.Email)
                .HasColumnName("email")
                .HasMaxLength(150)
                .IsRequired();

            builder.HasIndex(u => u.Email)
                .IsUnique();

            builder.Property(u => u.SenhaHash)
                .HasColumnName("senha_hash")
                .HasMaxLength(255)
                .IsRequired();

            builder.Property(u => u.DataNascimento)
                .HasColumnName("data_nascimento")
                .HasColumnType("date");

            builder.Property(u => u.Telefone)
                .HasColumnName("telefone")
                .HasMaxLength(20);

            builder.Property(u => u.Cidade)
                .HasColumnName("cidade")
                .HasMaxLength(100);

            builder.Property(u => u.Estado)
                .HasColumnName("estado")
                .HasMaxLength(2);

            builder.Property(u => u.Peso)
                .HasColumnName("peso")
                .HasColumnType("decimal(5,2)");

            builder.Property(u => u.Altura)
                .HasColumnName("altura")
                .HasColumnType("decimal(5,2)");

            builder.Property(u => u.TipoSanguineo)
                .HasColumnName("tipo_sanguineo")
                .HasMaxLength(3);

            builder.Property(u => u.FaseVida)
                .HasColumnName("fase_vida")
                .HasMaxLength(50);

            builder.Property(u => u.CreatedAt)
                .HasColumnName("criado_em")
                .HasColumnType("timestamp with time zone")
                .IsRequired();

            builder.Property(u => u.UpdatedAt)
                .HasColumnName("atualizado_em")
                .HasColumnType("timestamp with time zone");
        }
    }
}
