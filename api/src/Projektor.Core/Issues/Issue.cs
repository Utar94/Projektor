using Projektor.Core.Projects;

namespace Projektor.Core.Issues
{
  public class Issue : Aggregate
  {
    public Issue(int number, IssueType type, Guid userId) : base(userId)
    {
      Number = number;
      Type = type ?? throw new ArgumentNullException(nameof(type));
      TypeId = type.Id;
      Project = type.Project ?? throw new ArgumentException($"The {nameof(type.Project)} is required.", nameof(type));
      ProjectId = type.Project.Id;
    }
    private Issue() : base()
    {
    }

    public string? Description { get; set; }
    public DateTime? DueDate { get; set; }
    public int? Estimate { get; set; }
    public string Name { get; set; } = string.Empty;
    public int Number { get; set; }
    public Project? Project { get; set; }
    public int ProjectId { get; set; }
    public double? Score { get; set; }
    public IssueType? Type { get; set; }
    public int TypeId { get; set; }

    public override string ToString() => $"{Name} | {base.ToString()}";
  }
}
