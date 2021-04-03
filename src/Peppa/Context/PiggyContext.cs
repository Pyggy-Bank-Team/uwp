using Microsoft.EntityFrameworkCore;
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
            optionsBuilder.UseSqlite("Filename=piggy.db");
        }

        public DbSet<Account> Accounts { get; set; }     

        public DbSet<Category> Categories { get; set; }

        public DbSet<Operation> Operations { get; set; }
        
        public DbSet<User> Users { get; set; }
    }
}
