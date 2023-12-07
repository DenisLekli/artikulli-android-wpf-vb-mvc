
using ArtikullServices.Models;
using Microsoft.EntityFrameworkCore;

namespace ArtikullServices.Data
{
    public class ArtikullServicesDbContext : DbContext
    {
        public ArtikullServicesDbContext(DbContextOptions options)
        : base(options)
        {

        }
        public DbSet<Produkt> cases { get; set; }
    }
}
