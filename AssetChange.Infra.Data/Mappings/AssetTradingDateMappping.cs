using AssetChange.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AssetChange.Infra.Data.Mappings
{
    public class AssetTradingDateMappping : IEntityTypeConfiguration<AssetTradingDate>
    {
        public void Configure(EntityTypeBuilder<AssetTradingDate> builder)
        {
            builder
                .ToTable("AssetTradingDate")
                .HasKey(x => x.Id);

            builder
                .Property(x => x.Id)
                .IsRequired()
                .ValueGeneratedOnAdd();

            builder
                .Property(x => x.AssetId)
                .IsRequired();

            builder
                .Property(x => x.EventDate)
                .HasColumnType("datetime")
                .IsRequired(false);

            builder
                .Property(x => x.OpeningValue)
                .HasColumnType("money")
                .IsRequired(false);

            builder
                .HasOne(atd => atd.Asset)
                .WithMany(a => a.AssetTradingDates)
                .HasForeignKey(atd => atd.AssetId);
        }
    }
}
