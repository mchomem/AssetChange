using AssetChange.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AssetChange.Infra.Data.Mappings
{
    public class CurrentTradingPeriodMapping : IEntityTypeConfiguration<CurrentTradingPeriod>
    {
        public void Configure(EntityTypeBuilder<CurrentTradingPeriod> builder)
        {
            builder
                .ToTable("CurrentTradingPeriod")
                .HasKey(x => x.Id);

            builder
                .Property(x => x.Id)
                .IsRequired()
                .ValueGeneratedOnAdd();                

            builder
                .Property(x => x.AssetId)
                .IsRequired();

            builder
                .Property(x => x.Timezone)
                .HasColumnType("varchar")
                .HasMaxLength(10)
                .IsRequired();

            builder
                .Property(x => x.Start)
                .HasColumnType("datetime")
                .IsRequired();

            builder
                .Property(x => x.End)
                .HasColumnType("datetime")
                .IsRequired();

            builder
                .Property(x => x.Gmtoffset)
                .IsRequired();

            builder
                .Property(x => x.Type)
                .HasColumnType("varchar")
                .HasMaxLength(10)
                .IsRequired();

            builder
               .HasOne(ctp => ctp.Asset)
               .WithMany(a => a.CurrentTradingPeriods)
               .HasForeignKey(ctp => ctp.AssetId);
        }
    }
}
