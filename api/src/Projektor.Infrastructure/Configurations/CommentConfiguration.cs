using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Projektor.Core.Comments;

namespace Projektor.Infrastructure.Configurations
{
  internal class CommentConfiguration : AggregateConfiguration, IEntityTypeConfiguration<Comment>
  {
    public void Configure(EntityTypeBuilder<Comment> builder)
    {
      base.Configure(builder);
    }
  }
}
