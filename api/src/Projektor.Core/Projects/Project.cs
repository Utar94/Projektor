namespace Projektor.Core.Projects
{
  public class Project : Aggregate
  {
    public Project(string alias, Guid userId) : base(userId)
    {
      Alias = alias?.ToLowerInvariant() ?? throw new ArgumentNullException(nameof(alias));
    }
    private Project() : base()
    {
    }

    public string Alias { get; set; } = string.Empty;
    public string? Description { get; set; }
    public string Name { get; set; } = string.Empty;

    public override string ToString() => $"{Name} | {base.ToString()}";
  }
}
