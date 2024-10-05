
using Framework.Shared.IntegrationEvent.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using WalletService.Domain.Models.Entities;
using WalletService.Domain.Models.Entities.Ids;


namespace WalletService.Infra.Data.Mappings
{
    public class StockWalletMapping : IEntityTypeConfiguration<StockWallet>
    {
        public void Configure(EntityTypeBuilder<StockWallet> builder)
        {
            builder.ToTable("StockWallets");

            var converter = new ValueConverter<StockWalletId, Guid>(
                    id => id.Value,
                    guidValue => new StockWalletId(guidValue));

            builder.HasKey(e => e.StockWalletId);
            builder.Property(e => e.StockWalletId)
                .HasConversion(converter)
                .ValueGeneratedOnAdd();


            builder.Property(c => c.Symbol)
                         .IsRequired()
                         .HasColumnType("varchar(50)");

            builder.Property(c => c.Amount)
                         .IsRequired()
                         .HasColumnType("decimal(10,2)");

        }
    }
}
