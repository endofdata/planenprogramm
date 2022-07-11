using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.Extensions.Logging;
using Planenprogramm.EntityTypeConfiguration;
using System;

namespace Planenprogramm
{
	class Database : DbContext
	{
		public DbSet<Tarp> Tarps { get; set; }
		public DbSet<TarpType> TarpTypes { get; set; }
		public DbSet<TarpCategory> TarpCategories { get; set; }

		protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
		{
			base.OnConfiguring(optionsBuilder);

			optionsBuilder.UseSqlite(@"DataSource=D:\Gamer\Documents\Planenprogramm\Planenprogramm\data\tarps.sqlite");

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
		}
	}
}
