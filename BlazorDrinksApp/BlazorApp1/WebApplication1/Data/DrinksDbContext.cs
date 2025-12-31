using Microsoft.EntityFrameworkCore;
using WebApplication1.Model;

namespace WebApplication1.Data
{
    public class DrinksDbContext(DbContextOptions<DrinksDbContext> options) : DbContext(options)
    {
        public DbSet<Drink> Drinks { get; set; }
    }
}
