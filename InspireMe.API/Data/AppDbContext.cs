using InspireMe.API.Models;
using Microsoft.EntityFrameworkCore;

namespace InspireMe.API.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options) { }

        public DbSet<Quote> Quotes { get; set; }
    }
}
