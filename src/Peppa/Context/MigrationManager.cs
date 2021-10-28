using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Peppa.Context.Entities;
using Peppa.Interface;

namespace Peppa.Context
{
    public class MigrationManager : IMigrationManager
    {
        private const int LastVersion = 203;
        private readonly PiggyContext _context;
        private readonly Dictionary<int, Action> _migrations;

        public MigrationManager(PiggyContext context)
        {
            _context = context;
            _migrations = new Dictionary<int, Action<MigrationHistory>>
            {
                {202, (migration) => {_context.Database.ExecuteSqlCommand("alter table User add ExternalId text null;"); migration.AppVersion = 203; } } //Added ExternalId to user
            };
        }

        public void Migrate()
        {
            MigrationHistory lastMigration;
            try
            {
                lastMigration = _context.MigrationHistories.OrderByDescending(h => h.AppVersion).FirstOrDefault();
                if (lastMigration == null)
                {
                    _context.MigrationHistories.Add(new MigrationHistory { AppVersion = LastVersion });
                    _context.SaveChanges();
                    return;
                }
            }
            catch
            {
                _context.Database.ExecuteSqlCommand(@"CREATE TABLE ""MigrationHistory"" {Id INTEGER PRIMARY KEY, AppVersion INTEGER NOT NULL};");
                _context.Database.ExecuteSqlCommand("insert into table MigrationHistory (AppVersion) values (202);");
                lastMigration = _context.MigrationHistories.OrderByDescending(h => h.AppVersion).First();
            }


            foreach (var (key, migration) in _migrations)
            {
                if (lastMigration.AppVersion == key)
                {
                    migration();

                }
            }


            switch (lastMigration.AppVersion)
            {
                case 202:
                    AddedExternalIdentificationToUser();
                    lastMigration.AppVersion = 203;
                    _context.SaveChanges();
                    break;
            }
        }

        private void AddedExternalIdentificationToUser()
            => _context.Database.ExecuteSqlCommand("alter table User add ExternalId text null;");

        public void Dispose()
            => _context?.Dispose();
    }
}