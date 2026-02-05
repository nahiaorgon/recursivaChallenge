using Microsoft.EntityFrameworkCore;
using Horoscopo.Core.Entities;

namespace Horoscopo.Core.Repository
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<Historial> Historiales { get; set; }
    }
}