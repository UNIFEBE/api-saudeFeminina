using ApiSaudeFeminina.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ApiSaudeFeminina.Infrastructure.Configurations
{
    public class RegistroDiarioConfiguration : IEntityTypeConfiguration<RegistroDiario>
    {
        public void Configure(EntityTypeBuilder<RegistroDiario> builder)
        {
            builder.ToTable("registro_diario");

            builder.HasKey(r => r.Id);

            builder.Property(r => r.Id)
                .HasColumnName("id")
                .ValueGeneratedOnAdd();

            builder.Property(r => r.UsuarioId)
                .HasColumnName("usuario_id")
                .IsRequired();

            builder.Property(r => r.DataRegistro)
                .HasColumnName("data_registro")
                .HasColumnType("date")
                .IsRequired();

            builder.HasIndex(r => new { r.UsuarioId, r.DataRegistro })
                .IsUnique();

            builder.Property(r => r.Humor)
                .HasColumnName("humor")
                .HasMaxLength(50);

            builder.Property(r => r.Fluxo)
                .HasColumnName("fluxo")
                .HasMaxLength(20)
                .HasDefaultValue("Nenhum");

            builder.Property(r => r.Temperatura)
                .HasColumnName("temperatura")
                .HasColumnType("decimal(4,2)");

            builder.Property(r => r.Sintomas)
                .HasColumnName("sintomas")
                .HasColumnType("text[]");

            builder.Property(r => r.Observacoes)
                .HasColumnName("observacoes");

            builder.Property(r => r.CreatedAt)
                .HasColumnName("criado_em")
                .HasColumnType("timestamp with time zone")
                .IsRequired();

            // A tabela não tem atualizado_em
            builder.Ignore(r => r.UpdatedAt);

            builder.HasOne(r => r.Usuario)
                .WithMany()
                .HasForeignKey(r => r.UsuarioId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
