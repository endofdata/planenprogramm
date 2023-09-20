using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Tarps.Datalayer.Entities;

namespace Tarps.Datalayer.EntityTypeConfiguration
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
