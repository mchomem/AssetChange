using AssetChange.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AssetChange.Infra.Data.Mappings
{
    public class AssetMapping : IEntityTypeConfiguration<Asset>
    {
        public void Configure(EntityTypeBuilder<Asset> builder)
        {
            builder
                .ToTable("Asset")
                .HasKey(x => x.Id);

            builder
                .Property(x => x.Id)
                .IsRequired()
                .ValueGeneratedOnAdd();

            builder
                .Property(x => x.ImportedIn)
                .HasColumnType("datetime")
                .HasDefaultValueSql("getdate()")
                .IsRequired();

            builder
                .Property(x => x.Currency)
                .HasColumnType("varchar")
                .HasMaxLength(10)
                .IsRequired();

            builder
                .Property(x => x.Symbol)
                .HasColumnType("varchar")
                .HasMaxLength(50)
                .IsRequired();

            builder
                .Property(x => x.ExchangeName)
                .HasColumnType("varchar")
                .HasMaxLength(50)
                .IsRequired();

            builder
                .Property(x => x.InstrumentType)
                .HasColumnType("varchar")
                .HasMaxLength(10)
                .IsRequired();

            builder
                .Property(x => x.FirstTradeDate)
                .HasColumnType("datetime")
                .IsRequired();

            builder
                .Property(x => x.RegularMarketTime)
                .HasColumnType("datetime")
                .IsRequired();

            builder
                .Property(x => x.Gmtoffset)
                .IsRequired();

            builder
                .Property(x => x.Timezone)
                .HasColumnType("varchar")
                .HasMaxLength(5)
                .IsRequired();

            builder
                .Property(x => x.ExchangeTimezoneName)
                .HasColumnType("varchar")
                .HasMaxLength(50)
                .IsRequired();

            builder
                .Property(x => x.RegularMarketPrice)
                .HasColumnType("money")
                .IsRequired();

            builder
                .Property(x => x.ChartPreviousClose)
                .HasColumnType("money")
                .IsRequired();

            builder
                .Property(x => x.PreviousClose)
                .HasColumnType("money")
                .IsRequired();

            builder
                .Property(x => x.Scale)
                .IsRequired();

            builder
                .Property(x => x.PriceHint)
                .IsRequired();

            builder
                .Property(x => x.DataGranularity)
                .HasColumnType("varchar")
                .HasMaxLength(3)
                .IsRequired();

            builder
                .Property(x => x.Range)
                .HasColumnType("varchar")
                .HasMaxLength(3)
                .IsRequired();

            builder
                .Property(x => x.ValidRanges)
                .HasColumnType("varchar")
                .HasMaxLength(100)
                .IsRequired();
        }
    }
}
