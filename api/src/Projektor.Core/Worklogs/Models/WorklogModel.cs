using Projektor.Core.Models;

namespace Projektor.Core.Worklogs.Models
{
  public class WorklogModel : AggregateModel
  {
    public string? Description { get; set; }
    public DateTime EndedAt { get; set; }
    public DateTime StartedAt { get; set; }
  }
}
