
using Framework.Shared.IntegrationEvent.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using StockService.Domain.Models.Entities;
using StockService.Domain.Models.Entities.Ids;


namespace StockService.Infra.Data.Mappings
{
    public class StockResultTransactionMapping : IEntityTypeConfiguration<StockResultTransaction>
    {
        public void Configure(EntityTypeBuilder<StockResultTransaction> builder)
        {
            builder.ToTable("StockResultTransaction");

            var converter = new ValueConverter<StockResultTransactionId, Guid>(
                    id => id.Value,
                    guidValue => new StockResultTransactionId(guidValue));

            builder.HasKey(e => e.StockResultTransactionId);
            builder.Property(e => e.StockResultTransactionId)
                .HasConversion(converter)
                .ValueGeneratedOnAdd();

            var converterSockId = new ValueConverter<StockId, Guid>(
                    id => id.Value,
                    guidValue => new StockId(guidValue));
            builder.Property(c => c.StockId).HasConversion(converterSockId);

            //  builder.HasOne(c=> c.Stock).WithOne().HasForeignKey<Stock>(p=> p.StockId);

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
