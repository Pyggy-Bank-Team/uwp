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
        private readonly Dictionary<int, Action<MigrationHistory>> _migrations;//Use the dictionary for consistently update the database

        public MigrationManager(PiggyContext context)
        {
            _context = context;
            _migrations = new Dictionary<int, Action<MigrationHistory>>
            {
                {202, (migration) => {_context.Database.ExecuteSqlCommand("ALTER TABLE Users ADD ExternalId TEXT NULL;"); migration.AppVersion = 203;} } //Added ExternalId to user
            };
        }

        public void Migrate()
        {
            MigrationHistory lastMigration;
            try
            {
                //If app was installed on a new PC then don't need to create `MigrationHistories`
                lastMigration = _context.MigrationHistories.FirstOrDefault();
                if (lastMigration == null)
                {
                    _context.MigrationHistories.Add(new MigrationHistory { AppVersion = LastVersion });
                    _context.SaveChanges();
                    return;
                }
            }
            catch
            {
                _context.Database.ExecuteSqlCommand("CREATE TABLE MigrationHistories (Id INTEGER NOT NULL CONSTRAINT PK_MigrationHistories PRIMARY KEY AUTOINCREMENT, AppVersion INTEGER NOT NULL);");
                _context.Database.ExecuteSqlCommand("INSERT INTO MigrationHistories(AppVersion) VALUES(202);");
                lastMigration = _context.MigrationHistories.First();
            }


            foreach (var (key, migration) in _migrations)
            {
                if (lastMigration.AppVersion == key)
                {
                    migration(lastMigration);
                    _context.SaveChanges();
                }
            }
        }
    }
}