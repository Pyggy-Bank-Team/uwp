using System.Linq;
using Microsoft.EntityFrameworkCore;
using Peppa.Context.Entities;
using Peppa.Interface;

namespace Peppa.Context
{
    public class MigrationManager : IMigrationManager
    {
        private readonly PiggyContext _context;

        public MigrationManager(PiggyContext context)
            => _context = context;

        public void Migrate()
        {
            MigrationHistory lastMigration;
            try
            {
                lastMigration = _context.MigrationHistories.OrderByDescending(h => h.AppVersion).First();
            }
            catch
            {
                _context.Database.ExecuteSqlCommand("create table MigrationHistory {Id INTEGER PRIMARY KEY, AppVersion INTEGER NOT NULL};");
                _context.Database.ExecuteSqlCommand("insert into table MigrationHistory (AppVersion) values (202)");
                lastMigration = _context.MigrationHistories.OrderByDescending(h => h.AppVersion).First();
            }

            switch (lastMigration.AppVersion)
            {
                case 202:
                    AddedExternalIdentificationToUser();
                    lastMigration.AppVersion = 203;
                    break;
            }

            _context.SaveChanges();
        }

        private void AddedExternalIdentificationToUser()
            => _context.Database.ExecuteSqlCommand("alter table User add ExternalId text null;");

        public void Dispose()
            => _context?.Dispose();
    }
}