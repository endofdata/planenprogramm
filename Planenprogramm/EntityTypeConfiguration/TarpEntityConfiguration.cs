using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Planenprogramm.Entities;

namespace Planenprogramm.EntityTypeConfiguration
{
	class TarpEntityConfiguration : IEntityTypeConfiguration<Tarp>
	{
		public void Configure(EntityTypeBuilder<Tarp> builder)
		{
			builder.HasKey(tarp => tarp.Id);
			builder.Property(tarp => tarp.Id)
				.ValueGeneratedOnAdd();

			builder.HasOne(tarp => tarp.Category)
				.WithMany(cat => cat.Tarps);
		}
	}
}
