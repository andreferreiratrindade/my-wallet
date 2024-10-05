
using Framework.Shared.IntegrationEvent.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using StockService.Domain.Models.Entities;
using StockService.Domain.Models.Entities.Ids;


namespace StockService.Infra.Data.Mappings
{
    public class StockMapping : IEntityTypeConfiguration<Stock>
    {
        public void Configure(EntityTypeBuilder<Stock> builder)
        {
            builder.ToTable("Stocks");

            var converter = new ValueConverter<StockId, Guid>(
                    id => id.Value,
                    guidValue => new StockId(guidValue));

            builder.HasKey(e => e.StockId);
            builder.Property(e => e.StockId)
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
