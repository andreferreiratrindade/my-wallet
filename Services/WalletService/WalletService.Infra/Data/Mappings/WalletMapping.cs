
using Framework.Shared.IntegrationEvent.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using WalletService.Domain.Models.Entities;
using WalletService.Domain.Models.Entities.Ids;


namespace WalletService.Infra.Data.Mappings
{
    public class WalletMapping : IEntityTypeConfiguration<Wallet>
    {
        public void Configure(EntityTypeBuilder<Wallet> builder)
        {
            builder.ToTable("Wallets");

            var converter = new ValueConverter<WalletId, Guid>(
                    id => id.Value,
                    guidValue => new WalletId(guidValue));

            builder.HasKey(e => e.WalletId);
            builder.Property(e => e.WalletId)
                .HasConversion(converter)
                .ValueGeneratedOnAdd();


            builder.Property(c => c.Symbol)
                         .IsRequired()
                         .HasColumnType("varchar(50)");

            builder.Property(c => c.Name)
                         .IsRequired()
                         .HasColumnType("varchar(255)");

        }
    }
}
