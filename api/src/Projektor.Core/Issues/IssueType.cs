using Projektor.Core.Projects;

namespace Projektor.Core.Issues
{
  public class IssueType : Aggregate
  {
    public IssueType(Project project, Guid userId) : base(userId)
    {
      Project = project ?? throw new ArgumentNullException(nameof(project));
      ProjectId = project.Id;
    }
    private IssueType() : base()
    {
    }

    public string? Description { get; set; }
    public string Name { get; set; } = string.Empty;
    public Project? Project { get; set; }
    public int ProjectId { get; set; }

    public override string ToString() => $"{Name} | {base.ToString()}";
  }
}
