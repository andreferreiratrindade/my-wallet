
using Framework.Shared.IntegrationEvent.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using ForexService.Domain.Models.Entities;
using ForexService.Domain.Models.Entities.Ids;


namespace ForexService.Infra.Data.Mappings
{
    public class ForexResultTransactionMapping : IEntityTypeConfiguration<ForexResultTransaction>
    {
        public void Configure(EntityTypeBuilder<ForexResultTransaction> builder)
        {
            builder.ToTable("ForexResultTransaction");

            var converter = new ValueConverter<ForexResultTransactionId, Guid>(
                    id => id.Value,
                    guidValue => new ForexResultTransactionId(guidValue));

            builder.HasKey(e => e.ForexResultTransactionId);
            builder.Property(e => e.ForexResultTransactionId)
                .HasConversion(converter)
                .ValueGeneratedOnAdd();

            var converterSockId = new ValueConverter<ForexId, Guid>(
                    id => id.Value,
                    guidValue => new ForexId(guidValue));
            builder.Property(c => c.ForexId).HasConversion(converterSockId);

            //  builder.HasOne(c=> c.Forex).WithOne().HasForeignKey<Forex>(p=> p.ForexId);

            builder.Property(c => c.TotalAmount)
                        .IsRequired()
                        .HasColumnType("decimal(10,2)");

            builder.Property(c => c.TotalValue)
                    .IsRequired()
                    .HasColumnType("decimal(10,2)");

            builder.Property(c=> c.CreatedAt)
                    .IsRequired()
                     .HasColumnType("datetime2");

            builder.Property(c=> c.UpdatedAt)
                    .IsRequired()
                    .HasColumnType("datetime2");


        }
    }
}
