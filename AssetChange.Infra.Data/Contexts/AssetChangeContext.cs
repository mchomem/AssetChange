using AssetChange.Domain.Entities;
using AssetChange.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System.Diagnostics;

namespace AssetChange.Infra.Data.Contexts
{
    public class AssetChangeContext : DbContext
    {
        public DbSet<Asset> Asset { get; set; }
        public DbSet<AssetTradingDate> AssetTradingDate { get; set; }

        public AssetChangeContext() : base() {}

        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            options
                .UseSqlServer(AppSettings.Database.ConnectionStrings)
                .LogTo(message => Debug.WriteLine(message), LogLevel.Information)
                .EnableSensitiveDataLogging()
                .EnableDetailedErrors();
        }

        protected override void OnModelCreating(ModelBuilder builder)
            => builder.ApplyConfigurationsFromAssembly(typeof(AssetChangeContext).Assembly);        
    }
}
