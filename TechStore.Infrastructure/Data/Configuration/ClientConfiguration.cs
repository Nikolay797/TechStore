using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TechStore.Infrastructure.Data.Models;

namespace TechStore.Infrastructure.Data.Configuration
{
    internal class ClientConfiguration : IEntityTypeConfiguration<Client>
    {
        public void Configure(EntityTypeBuilder<Client> builder)
        {
            builder.HasData(this.CreateClients());
        }

        private List<Client> CreateClients()
        {
            var clients = new List<Client>();

            var client = new Client()
            {
                Id = 1,
                UserId = "0b129438-03c0-4f93-8d80-16fa6d4afa54",
                CountOfPurchases = 3,
            };
            
            clients.Add(client);
            return clients;
        }
    }
}
