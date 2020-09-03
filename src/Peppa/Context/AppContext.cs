using Microsoft.EntityFrameworkCore;
using Peppa.Entities;
using Peppa.Models;

namespace Peppa.Context
{
    public sealed class AppContext : DbContext
    {
        public AppContext()
        {
            Database.EnsureCreated();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite("Filename=Costs.db");
        }

        public DbSet<Account> Accounts { get; set; }

        public DbSet<CostModel> Costs { get; set; }

        public DbSet<CategoryModel> Categories { get; set; }

        public DbSet<BalanceModel> Balance { get; set; }
    }
}
