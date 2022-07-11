using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using System;
using System.IO;
using System.Linq;

namespace Planenprogramm
{
	internal class DatabaseFactory : IDesignTimeDbContextFactory<Database>
    {
        public Database CreateDbContext(string[] args)
        {
            var dataDirectory = args.FirstOrDefault();

            if (dataDirectory == null)
            {
                throw new InvalidOperationException("Es wurde kein Datenverzeichnis angegeben.");
            }

            var databasePath = Path.Combine(dataDirectory, "tarps.sqlite");
            var optionsBuilder = new DbContextOptionsBuilder<Database>();
            optionsBuilder.UseSqlite($"DataSource={databasePath}");

            return new Database(optionsBuilder.Options);
        }
    }
}
