using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Planenprogramm.EntityTypeConfiguration
{
	class TarpTypeEntityConfiguration : IEntityTypeConfiguration<TarpType>
	{
		public void Configure(EntityTypeBuilder<TarpType> builder)
		{
			builder.HasKey(ttp => ttp.TarpTypeId);
			builder.Property(ttp => ttp.TarpTypeId)
				.ValueGeneratedOnAdd();

			builder.HasMany(cat => cat.Tarps)
				.WithOne(tarp => tarp.TarpType);
		}
	}
}
