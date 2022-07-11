using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Planenprogramm.Entities;

namespace Planenprogramm.EntityTypeConfiguration
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
