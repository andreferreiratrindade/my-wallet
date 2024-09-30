
using Framework.Shared.IntegrationEvent.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using StockService.Domain.Models.Entities;


namespace StockService.Infra.Data.Mappings
{
    public class TransactionMapping : IEntityTypeConfiguration<Transaction>
    {
        public void Configure(EntityTypeBuilder<Transaction> builder)
        {
            builder.ToTable("Transactions");

            builder.HasKey(c => c.AggregateId);
            builder.Property(c => c.AggregateId).HasColumnName("Id");

            builder.Property(c => c.InvestmentDate)
                .IsRequired()
                .HasColumnType("datetime2");

            builder.Property(c => c.Symbol)
                         .IsRequired()
                         .HasColumnType("varchar(50)");

            builder.Property(c => c.Amount)
                        .IsRequired()
                        .HasColumnType("decimal(10,2)");

            builder.Property(c => c.Value)
         .IsRequired()
         .HasColumnType("decimal(10,2)");

            builder.Property(c => c.TypeOperationInvestment)
                .IsRequired()
                .HasConversion(new EnumToNumberConverter<TypeOperationInvestment, byte>());



        }
    }
}
