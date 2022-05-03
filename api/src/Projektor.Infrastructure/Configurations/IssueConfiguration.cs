using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Projektor.Core.Issues;

namespace Projektor.Infrastructure.Configurations
{
  internal class IssueConfiguration : AggregateConfiguration, IEntityTypeConfiguration<Issue>
  {
    public void Configure(EntityTypeBuilder<Issue> builder)
    {
      base.Configure(builder);

      builder.HasIndex(x => x.Name);
      builder.HasIndex(x => new { x.ProjectId, x.Number }).IsUnique();

      builder.Property(x => x.Name).HasMaxLength(100);
    }
  }
}
