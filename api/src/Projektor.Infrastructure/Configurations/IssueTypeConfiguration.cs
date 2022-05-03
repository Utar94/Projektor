using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Projektor.Core.Issues;

namespace Projektor.Infrastructure.Configurations
{
  internal class IssueTypeConfiguration : AggregateConfiguration, IEntityTypeConfiguration<IssueType>
  {
    public void Configure(EntityTypeBuilder<IssueType> builder)
    {
      base.Configure(builder);

      builder.HasIndex(x => x.Name);

      builder.Property(x => x.Name).HasMaxLength(100);
    }
  }
}
