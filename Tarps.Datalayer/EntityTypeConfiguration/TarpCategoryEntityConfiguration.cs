using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Tarps.Datalayer.Entities;

namespace Tarps.Datalayer.EntityTypeConfiguration
{
	class TarpCategoryEntityConfiguration : IEntityTypeConfiguration<TarpCategory>
	{
		public void Configure(EntityTypeBuilder<TarpCategory> builder)
		{
			builder.HasKey(cat => cat.Id);
			builder.Property(cat => cat.Id)
				.ValueGeneratedOnAdd();

			builder.HasMany(cat => cat.Tarps)
				.WithOne(tarp => tarp.Category);
		}
	}
}
