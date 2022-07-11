using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Planenprogramm.EntityTypeConfiguration
{
	class TarpCategoryEntityConfiguration : IEntityTypeConfiguration<TarpCategory>
	{
		public void Configure(EntityTypeBuilder<TarpCategory> builder)
		{
			builder.HasKey(cat => cat.TarpCategoryId);
			builder.Property(cat => cat.TarpCategoryId)
				.ValueGeneratedOnAdd();

			builder.HasMany(cat => cat.Tarps)
				.WithOne(tarp => tarp.TarpCategory);
		}
	}
}
