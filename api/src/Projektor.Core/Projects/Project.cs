using Projektor.Core.Issues;

namespace Projektor.Core.Projects
{
  public class Project : Aggregate
  {
    public Project(string key, Guid userId) : base(userId)
    {
      Key = key?.ToLowerInvariant() ?? throw new ArgumentNullException(nameof(key));
    }
    private Project() : base()
    {
    }

    public string? Description { get; set; }
    public string Key { get; set; } = string.Empty;
    public string Name { get; set; } = string.Empty;

    public ICollection<IssueType> IssueTypes { get; set; } = new List<IssueType>();

    public override string ToString() => $"{Name} | {base.ToString()}";
  }
}
