using Microsoft.EntityFrameworkCore;
using Peppa.Context.Entities;
using piggy_bank_uwp.Context.Entities;

namespace Peppa.Context
{
    public sealed class PiggyContext : DbContext
    {
        public PiggyContext()
        {
            Database.EnsureCreated();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite("Filename=Costs.db");
        }

        public DbSet<Account> Accounts { get; set; }     

        public DbSet<Category> Categories { get; set; }

        public DbSet<Operation> Operations { get; set; }
    }
}
