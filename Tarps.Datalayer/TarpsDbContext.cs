using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.Extensions.Logging;
using Tarps.Datalayer.Entities;
using Tarps.Datalayer.EntityTypeConfiguration;

namespace Tarps.Datalayer
{
	public class TarpsDbContext : DbContext
	{
		public DbSet<Tarp> Tarps
		{
			get; set;
		} = null!;

		public DbSet<TarpType> TarpTypes
		{
			get; set;
		} = null!;

		public DbSet<TarpCategory> Categories
		{
			get; set;
		} = null!;

		public DbSet<Damage> Damages
		{
			get; set;
		} = null!;

		public DbSet<TarpDamage> TarpDamages
		{
			get; set;
		} = null!;

		public TarpsDbContext(DbContextOptions options) : base(options)
		{
			
		}

		protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
		{
			base.OnConfiguring(optionsBuilder);

			optionsBuilder.UseSqlite();

			optionsBuilder.EnableSensitiveDataLogging(sensitiveDataLoggingEnabled: true);
			optionsBuilder.EnableDetailedErrors(detailedErrorsEnabled: true);
			optionsBuilder.LogTo(text => Console.WriteLine(text), LogLevel.Error, DbContextLoggerOptions.DefaultWithUtcTime);
		}

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			base.OnModelCreating(modelBuilder);

			new TarpEntityConfiguration().Configure(modelBuilder.Entity<Tarp>());
			new TarpTypeEntityConfiguration().Configure(modelBuilder.Entity<TarpType>());
			new TarpCategoryEntityConfiguration().Configure(modelBuilder.Entity<TarpCategory>());
			new DamageEntityConfiguration().Configure(modelBuilder.Entity<Damage>());
			new TarpDamageEntityConfiguration().Configure(modelBuilder.Entity<TarpDamage>());
		}
	}
}
