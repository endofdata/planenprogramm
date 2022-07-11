using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Planenprogramm.Entities;

namespace Planenprogramm.EntityTypeConfiguration
{
	class TarpTypeEntityConfiguration : IEntityTypeConfiguration<TarpType>
	{
		public void Configure(EntityTypeBuilder<TarpType> builder)
		{
			builder.HasKey(ttp => ttp.Id);
			builder.Property(ttp => ttp.Id)
				.ValueGeneratedOnAdd();

			builder.HasMany(ttp => ttp.Categories)
				.WithOne(cat => cat.TarpType);
		}
	}
}
