using Microsoft.EntityFrameworkCore;
using PokeApiV2.Server.Models;

namespace PokeApiV2.Server.DbContexts
{
    public class PokeApiDbContext(DbContextOptions options) : DbContext(options)
    {
        public DbSet<User> Users { get; set; } = null!;
    }
}
