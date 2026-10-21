using ApiSaudeFeminina.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ApiSaudeFeminina.Infrastructure.Configurations
{
    public class ConsultaConfiguration : IEntityTypeConfiguration<Consulta>
    {
        public void Configure(EntityTypeBuilder<Consulta> builder)
        {
            builder.ToTable("consulta");

            builder.HasKey(c => c.Id);
            builder.Property(c => c.Id)
                .HasColumnName("id");

            builder.Property(c => c.UsuarioId)
                .IsRequired()
                .HasColumnName("usuario_id");

            builder.Property(c => c.Especialidade)
                .IsRequired()
                .HasMaxLength(20)
                .HasColumnName("especialidade");

            builder.Property(c => c.DataConsulta)
                .IsRequired()
                .HasColumnType("date")
                .HasColumnName("data_consulta");

            builder.Property(c => c.Status)
                .IsRequired()
                .HasMaxLength(20)
                .HasDefaultValue("Confirmada")
                .HasColumnName("status");

            builder.Property(c => c.CreatedAt)
                .HasColumnName("criado_em");

            builder.Property(c => c.UpdatedAt)
                .HasColumnName("atualizado_em");

            builder.HasOne<Usuario>()
                .WithMany()
                .HasForeignKey(c => c.UsuarioId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
