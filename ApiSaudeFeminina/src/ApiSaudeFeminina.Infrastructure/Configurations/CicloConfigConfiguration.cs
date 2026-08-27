using ApiSaudeFeminina.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ApiSaudeFeminina.Infrastructure.Configurations
{
    public class CicloConfigConfiguration : IEntityTypeConfiguration<CicloConfig>
    {
        public void Configure(EntityTypeBuilder<CicloConfig> builder)
        {
            builder.ToTable("ciclo_config");

            builder.HasKey(c => c.Id);

            builder.Property(c => c.Id)
                .HasColumnName("id")
                .ValueGeneratedOnAdd();

            builder.Property(c => c.UsuarioId)
                .HasColumnName("usuario_id")
                .IsRequired();

            builder.HasIndex(c => c.UsuarioId)
                .IsUnique();

            builder.Property(c => c.DataUltimaMenstruacao)
                .HasColumnName("data_ultima_menstruacao")
                .HasColumnType("date")
                .IsRequired();

            builder.Property(c => c.DuracaoCiclo)
                .HasColumnName("duracao_ciclo")
                .IsRequired()
                .HasDefaultValue(28);

            builder.Property(c => c.DuracaoMenstruacao)
                .HasColumnName("duracao_menstruacao")
                .IsRequired()
                .HasDefaultValue(5);

            builder.Property(c => c.CicloRegular)
                .HasColumnName("ciclo_regular")
                .HasDefaultValue(true);

            builder.Property(c => c.UpdatedAt)
                .HasColumnName("atualizado_em")
                .HasColumnType("timestamp with time zone");

            // Ignora CreatedAt pois a tabela ciclo_config não tem esse campo
            builder.Ignore(c => c.CreatedAt);

            builder.HasOne(c => c.Usuario)
                .WithOne()
                .HasForeignKey<CicloConfig>(c => c.UsuarioId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
