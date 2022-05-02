using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Projektor.Core.Projects;

namespace Projektor.Infrastructure.Configurations
{
  internal class ProjectConfiguration : AggregateConfiguration, IEntityTypeConfiguration<Project>
  {
    public void Configure(EntityTypeBuilder<Project> builder)
    {
      base.Configure(builder);

      builder.HasIndex(x => x.Alias).IsUnique();
      builder.HasIndex(x => x.Name);

      builder.Property(x => x.Alias).HasMaxLength(12);
      builder.Property(x => x.Name).HasMaxLength(100);
    }
  }
}
