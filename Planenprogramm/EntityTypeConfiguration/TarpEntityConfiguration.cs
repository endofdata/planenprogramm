using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Planenprogramm.EntityTypeConfiguration
{
	class TarpEntityConfiguration : IEntityTypeConfiguration<Tarp>
	{
		public void Configure(EntityTypeBuilder<Tarp> builder)
		{
			builder.HasKey(tarp => tarp.TarpId);
			builder.Property(tarp => tarp.TarpId)
				.ValueGeneratedOnAdd();

			builder.HasOne(tarp => tarp.TarpCategory)
				.WithMany(cat => cat.Tarps);

			builder.HasOne(tarp => tarp.TarpType)
				.WithMany(ttype => ttype.Tarps);
		}
	}
}
