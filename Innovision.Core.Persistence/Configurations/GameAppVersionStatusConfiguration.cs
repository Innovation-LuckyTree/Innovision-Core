using Microsoft.EntityFrameworkCore;
using Innovision.Core.Domain.Entity;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Innovision.Core.Persistence.Configurations;

public class GameAppVersionStatusConfiguration : IEntityTypeConfiguration<GameAppVersionStatus>
{
	public void Configure(EntityTypeBuilder<GameAppVersionStatus> builder)
	{
		builder.ToTable("GameAppVersionStatus");
		builder.HasKey(e => e.StatusId);

		builder.Property(e => e.StatusId)
			.UseIdentityColumn(1, 1);

		builder.Property(e => e.Name)
			.IsRequired();
	}
}