using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Tarps.Datalayer.Entities;

namespace Tarps.Datalayer.EntityTypeConfiguration
{
	class TarpDamageEntityConfiguration : IEntityTypeConfiguration<TarpDamage>
	{
		public void Configure(EntityTypeBuilder<TarpDamage> builder)
		{
			builder.HasKey(td => td.Id);
			builder.Property(td => td.Id)
				.ValueGeneratedOnAdd();

			builder.HasOne(td => td.Damage)
				.WithMany(d => d.TarpDamages);

			builder.HasOne(td => td.Tarp)
				.WithMany(t => t.TarpDamages);
		}
	}
}
