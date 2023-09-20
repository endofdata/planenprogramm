using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Options;

namespace Tarps.Datalayer
{
	public class TarpsDbContextFactory : IDesignTimeDbContextFactory<TarpsDbContext>, IDbContextFactory<TarpsDbContext>
    {
		private readonly DbContextOptions<TarpsDbContext>? _options;

		public TarpsDbContextFactory(IOptions<DbContextOptions<TarpsDbContext>>? options = null)
        {
            _options = options?.Value;
        }

        public TarpsDbContext CreateDbContext(string[] args)
        {
            var dataDirectory = args.FirstOrDefault();

            if (dataDirectory == null)
            {
                throw new InvalidOperationException("Es wurde kein Datenverzeichnis angegeben.");
            }

            var databasePath = Path.Combine(dataDirectory, "tarps.sqlite");
            var optionsBuilder = new DbContextOptionsBuilder<TarpsDbContext>();
            optionsBuilder.UseSqlite($"DataSource={databasePath}");

            return new TarpsDbContext(optionsBuilder.Options);
        }

		public TarpsDbContext CreateDbContext()
		{
            if (_options == null)
            {
                throw new InvalidOperationException($"When using {typeof(TarpsDbContextFactory)} as {typeof(IDbContextFactory<TarpsDbContext>)}, " +
                    $"an instance of {typeof(IOptions<DbContextOptions<TarpsDbContext>>)} must be passed to the constructor.");
            }
            return new TarpsDbContext(_options);
        }
	}
}
