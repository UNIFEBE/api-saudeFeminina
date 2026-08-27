using ApiSaudeFeminina.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ApiSaudeFeminina.Infrastructure.Configurations
{
    public class CicloRegistroConfiguration : IEntityTypeConfiguration<CicloRegistro>
    {
        public void Configure(EntityTypeBuilder<CicloRegistro> builder)
        {
            builder.ToTable("ciclo_registro");

            builder.HasKey(r => r.Id);

            builder.Property(r => r.Id)
                .HasColumnName("id")
                .ValueGeneratedOnAdd();

            builder.Property(r => r.UsuarioId)
                .HasColumnName("usuario_id")
                .IsRequired();

            builder.Property(r => r.DataInicio)
                .HasColumnName("data_inicio")
                .HasColumnType("date")
                .IsRequired();

            builder.Property(r => r.DataFim)
                .HasColumnName("data_fim")
                .HasColumnType("date");

            builder.Property(r => r.DuracaoCiclo)
                .HasColumnName("duracao_ciclo");

            builder.Property(r => r.Observacoes)
                .HasColumnName("observacoes");

            builder.Property(r => r.CreatedAt)
                .HasColumnName("criado_em")
                .HasColumnType("timestamp with time zone")
                .IsRequired();

            builder.Property(r => r.UpdatedAt)
                .HasColumnName("atualizado_em")
                .HasColumnType("timestamp with time zone");

            builder.HasOne(r => r.Usuario)
                .WithMany()
                .HasForeignKey(r => r.UsuarioId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
