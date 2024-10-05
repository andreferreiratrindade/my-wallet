
using Framework.Shared.IntegrationEvent.Enums;
using Google.Protobuf.WellKnownTypes;
using MassTransit;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using StockService.Domain.Models.Entities;
using StockService.Domain.Models.Entities.Ids;


namespace StockService.Infra.Data.Mappings
{
    public class TransactionMapping : IEntityTypeConfiguration<TransactionStock>
    {
        public void Configure(EntityTypeBuilder<TransactionStock> builder)
        {
            builder.ToTable("TransactionStock");

            var converter = new ValueConverter<TransactionStockId, Guid>(
                    id => id.Value,
                    guidValue => new TransactionStockId(guidValue));

            builder.HasKey(e => e.TransactionStockId);
            builder.Property(e => e.TransactionStockId)
                .HasConversion(converter)
                .ValueGeneratedOnAdd();

            builder.Property(c => c.InvestmentDate)
                .IsRequired()
                .HasColumnType("datetime2");

            var converterSockId = new ValueConverter<StockId, Guid>(
                    id => id.Value,
                    guidValue => new StockId(guidValue));
            builder.Property(c=> c.StockId).HasConversion(converterSockId);

            //uilder.HasOne(c=> c.Stock).WithOne().HasForeignKey<Stock>(p=> p.StockId);


            builder.Property(c => c.Amount)
                        .IsRequired()
                        .HasColumnType("decimal(10,2)");

            builder.Property(c => c.Value)
         .IsRequired()
         .HasColumnType("decimal(10,2)");

            builder.Property(c => c.TypeOperationInvestment)
                .IsRequired()
                .HasConversion(new EnumToNumberConverter<TypeOperationInvestment, byte>());

                            builder.Property(c=> c.CreatedAt)
                    .IsRequired()
                     .HasColumnType("datetime2");

            builder.Property(c=> c.UpdatedAt)
                    .IsRequired()
                    .HasColumnType("datetime2");
        }
    }
}
