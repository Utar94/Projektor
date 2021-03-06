using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Projektor.Core;

namespace Projektor.Infrastructure.Configurations
{
  internal class AggregateConfiguration
  {
    public virtual void Configure<T>(EntityTypeBuilder<T> builder) where T : Aggregate
    {
      builder.HasIndex(x => x.CreatedById);
      builder.HasIndex(x => x.Deleted);
      builder.HasIndex(x => x.DeletedById);
      builder.HasIndex(x => x.UpdatedById);
      builder.HasIndex(x => x.Uuid).IsUnique();

      builder.Property(x => x.CreatedAt).HasDefaultValueSql("now()");
      builder.Property(x => x.Deleted).HasDefaultValue(false);
      builder.Property(x => x.Version).HasDefaultValue(0);
      builder.Property(x => x.Uuid).HasDefaultValueSql("uuid_generate_v4()");
    }
  }
}
