using Microsoft.EntityFrameworkCore;
using TechStore.Infrastructure.Data.Models;
using TechStore.Infrastructure.Data.Models.AttributesClasses;
using Television = TechStore.Infrastructure.Data.Models.Television;
using Type = TechStore.Infrastructure.Data.Models.AttributesClasses.Type;

using static TechStore.DataImport.Constants.ConfigurationConstants;

namespace TechStore.DataImport
{
    internal class TechStoreDbContext : DbContext
    {
        internal DbSet<Laptop> Laptops { get; set; } = null!;
        internal DbSet<Television> Televisions { get; set; } = null!;
        internal DbSet<Keyboard> Keyboards { get; set; } = null!;
        internal DbSet<Mouse> Mice { get; set; } = null!;
        internal DbSet<Headphone> Headphones { get; set; } = null!;
        internal DbSet<SmartWatch> SmartWatches { get; set; } = null!;
        internal DbSet<Brand> Brands { get; set; } = null!;
        internal DbSet<Color> Colors { get; set; } = null!;
        internal DbSet<CPU> CPUs { get; set; } = null!;
        internal DbSet<DisplayCoverage> DisplayCoverages { get; set; } = null!;
        internal DbSet<DisplaySize> DisplaySizes { get; set; } = null!;
        internal DbSet<DisplayTechnology> DisplayTechnologies { get; set; } = null!;
        internal DbSet<Format> Formats { get; set; } = null!;
        internal DbSet<RAM> RAMs { get; set; } = null!;
        internal DbSet<Resolution> Resolutions { get; set; } = null!;
        internal DbSet<Sensitivity> Sensitivities { get; set; } = null!;
        internal DbSet<SSDCapacity> SSDCapacities { get; set; } = null!;
        internal DbSet<Type> Types { get; set; } = null!;
        internal DbSet<VideoCard> VideoCards { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer(ConnectionString);
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }
}
