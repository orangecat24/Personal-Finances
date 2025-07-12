using Microsoft.EntityFrameworkCore;

namespace PersonalFinances.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }
        public DbSet<Models.Expense> Expenses { get; set; } 
    }
}
