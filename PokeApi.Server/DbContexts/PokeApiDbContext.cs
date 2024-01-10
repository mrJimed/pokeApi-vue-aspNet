using Microsoft.EntityFrameworkCore;
using PokeApi.Server.Models;

namespace PokeApi.Server.DbContexts
{
    public class PokeApiDbContext : DbContext
    {
        public DbSet<User> Users { get; set; } = null!;

        public PokeApiDbContext(DbContextOptions options) : base(options)
        {
            Database.EnsureCreated();
        }
    }
}
