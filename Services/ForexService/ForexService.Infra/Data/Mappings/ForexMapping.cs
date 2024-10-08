
using Framework.Shared.IntegrationEvent.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using ForexService.Domain.Models.Entities;
using ForexService.Domain.Models.Entities.Ids;


namespace ForexService.Infra.Data.Mappings
{
    public class ForexMapping : IEntityTypeConfiguration<Forex>
    {
        public void Configure(EntityTypeBuilder<Forex> builder)
        {
            builder.ToTable("Forexs");

            var converter = new ValueConverter<ForexId, Guid>(
                    id => id.Value,
                    guidValue => new ForexId(guidValue));

            builder.HasKey(e => e.ForexId);
            builder.Property(e => e.ForexId)
                .HasConversion(converter)
                .ValueGeneratedOnAdd();


            builder.Property(c => c.Symbol)
                         .IsRequired()
                         .HasColumnType("varchar(50)");

            builder.Property(c => c.Name)
                         .IsRequired()
                         .HasColumnType("varchar(255)");

                                     builder.Property(c=> c.CreatedAt)
                    .IsRequired()
                     .HasColumnType("datetime2");

            builder.Property(c=> c.UpdatedAt)
                    .IsRequired()
                    .HasColumnType("datetime2");

        }
    }
}
