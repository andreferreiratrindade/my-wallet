
using Framework.Shared.IntegrationEvent.Enums;
using Google.Protobuf.WellKnownTypes;
using MassTransit;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using ForexService.Domain.Models.Entities;
using ForexService.Domain.Models.Entities.Ids;


namespace ForexService.Infra.Data.Mappings
{
    public class TransactionMapping : IEntityTypeConfiguration<TransactionForex>
    {
        public void Configure(EntityTypeBuilder<TransactionForex> builder)
        {
            builder.ToTable("TransactionForex");

            var converter = new ValueConverter<TransactionForexId, Guid>(
                    id => id.Value,
                    guidValue => new TransactionForexId(guidValue));

            builder.HasKey(e => e.TransactionForexId);
            builder.Property(e => e.TransactionForexId)
                .HasConversion(converter)
                .ValueGeneratedOnAdd();

            builder.Property(c => c.InvestmentDate)
                .IsRequired()
                .HasColumnType("datetime2");

            var converterSockId = new ValueConverter<ForexId, Guid>(
                    id => id.Value,
                    guidValue => new ForexId(guidValue));
            builder.Property(c=> c.ForexId).HasConversion(converterSockId);

            //uilder.HasOne(c=> c.Forex).WithOne().HasForeignKey<Forex>(p=> p.ForexId);


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
