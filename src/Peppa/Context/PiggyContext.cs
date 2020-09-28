using Microsoft.EntityFrameworkCore;
using Peppa.Models;
using Peppa.Context.Entities;

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

        public DbSet<CostModel> Costs { get; set; }

        public DbSet<Category> Categories { get; set; }

        public DbSet<BalanceModel> Balance { get; set; }
    }
}
