using AutoMapper;
using Projektor.Core.Comments;
using Projektor.Core.Comments.Models;
using Projektor.Core.Models;

namespace Projektor.Core.Mapping
{
  internal class CommentProfile : Profile
  {
    public CommentProfile()
    {
      CreateMap<Comment, CommentModel>()
        .IncludeBase<Aggregate, AggregateModel>();
    }
  }
}
