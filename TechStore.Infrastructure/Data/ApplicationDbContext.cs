using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using TechStore.Infrastructure.Data.Configuration;
using TechStore.Infrastructure.Data.Models;
using TechStore.Infrastructure.Data.Models.Account;
using TechStore.Infrastructure.Data.Models.AttributesClasses;
using Type = TechStore.Infrastructure.Data.Models.AttributesClasses.Type;

namespace TechStore.Infrastructure.Data
{
    public class ApplicationDbContext : IdentityDbContext<User>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
            
        }

        public DbSet<Laptop> Laptops { get; set; } = null!;
        public DbSet<Client> Clients { get; set; } = null!;
        public DbSet<Keyboard> Keyboards { get; set; } = null!;
        public DbSet<Television> Televisions { get; set; } = null!;
        public DbSet<SmartWatch> SmartWatches { get; set; } = null!;
        public DbSet<Headphone> Headphones { get; set; } = null!;
        public DbSet<Mouse> Mice { get; set; } = null!;
        public DbSet<Brand> Brands { get; set; } = null!;
        public DbSet<Color> Colors { get; set; } = null!;
        public DbSet<CPU> CPUs { get; set; } = null!;
        public DbSet<DisplayCoverage> DisplayCoverages { get; set; } = null!;
        public DbSet<DisplaySize> DisplaySizes { get; set; } = null!;
        public DbSet<DisplayTechnology> DisplayTechnologies { get; set; } = null!;
        public DbSet<Format> Formats { get; set; } = null!;
        public DbSet<RAM> RAMs { get; set; } = null!;
        public DbSet<Resolution> Resolutions { get; set; } = null!;
        public DbSet<Sensitivity> Sensitivities { get; set; } = null!;
        public DbSet<SSDCapacity> SSDCapacities { get; set; } = null!;
        public DbSet<Type> Types { get; set; } = null!;
        public DbSet<VideoCard> VideoCards { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.ApplyConfiguration(new RoleConfiguration());
            builder.ApplyConfiguration(new UserConfiguration());

            base.OnModelCreating(builder);
        }
    }
}
