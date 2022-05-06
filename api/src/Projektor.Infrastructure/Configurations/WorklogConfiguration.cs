using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Projektor.Core.Worklogs;

namespace Projektor.Infrastructure.Configurations
{
  internal class WorklogConfiguration : AggregateConfiguration, IEntityTypeConfiguration<Worklog>
  {
    public void Configure(EntityTypeBuilder<Worklog> builder)
    {
      base.Configure(builder);

      builder.HasCheckConstraint("CHK_Worklogs_StartedEnded", @"""StartedAt"" < ""EndedAt""");
    }
  }
}
