﻿using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Projektor.Core.Issues;

namespace Projektor.Infrastructure.Configurations
{
  internal class IssueConfiguration : AggregateConfiguration, IEntityTypeConfiguration<Issue>
  {
    public void Configure(EntityTypeBuilder<Issue> builder)
    {
      base.Configure(builder);

      builder.HasIndex(x => x.ClosedAt);
      builder.HasIndex(x => x.ClosedById);
      builder.HasIndex(x => x.Name);
      builder.HasIndex(x => x.Priority);
      builder.HasIndex(x => x.Resolution);
      builder.HasIndex(x => new { x.ProjectId, x.Number }).IsUnique();

      builder.Property(x => x.Name).HasMaxLength(100);
      builder.Property(x => x.Priority).HasDefaultValue(Priority.Medium);
      builder.Property(x => x.Resolution).HasDefaultValue(Resolution.Unresolved);
    }
  }
}
