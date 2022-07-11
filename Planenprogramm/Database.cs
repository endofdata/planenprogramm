using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.Extensions.Logging;
using Planenprogramm.Entities;
using Planenprogramm.EntityTypeConfiguration;
using System;

namespace Planenprogramm
{
	class Database : DbContext
	{
		public DbSet<Tarp> Tarps
		{
			get; set;
		}
		public DbSet<TarpType> TarpTypes
		{
			get; set;
		}
		public DbSet<TarpCategory> Categories
		{
			get; set;
		}

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
		public Database(DbContextOptions options) : base(options)
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
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
		}
	}
}
