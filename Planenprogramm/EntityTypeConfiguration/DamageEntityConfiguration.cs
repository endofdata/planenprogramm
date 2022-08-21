using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Planenprogramm.Entities;

namespace Planenprogramm.EntityTypeConfiguration
{
	internal class DamageEntityConfiguration : IEntityTypeConfiguration<Damage>
	{
		public void Configure(EntityTypeBuilder<Damage> builder)
		{
			builder.HasKey(d => d.Id);
			builder.Property(d => d.Id)
				.ValueGeneratedOnAdd();

			builder.HasMany(d => d.TarpDamages)
				.WithOne(td => td.Damage);
		}
	}
}
