#nullable disable
using Logitar.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Projektor.Core.Issues;
using Projektor.Core.Projects;
using Projektor.Core.Worklogs;
using System.Reflection;

namespace Projektor.Infrastructure
{
  public class ProjektorDbContext : IdentityDbContext
  {
    public ProjektorDbContext(DbContextOptions<ProjektorDbContext> options) : base(options)
    {
    }

    public DbSet<Issue> Issues { get; set; }
    public DbSet<IssueType> IssueTypes { get; set; }
    public DbSet<Project> Projects { get; set; }
    public DbSet<Worklog> Worklogs { get; set; }

    protected override void OnModelCreating(ModelBuilder builder)
    {
      base.OnModelCreating(builder);

      builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
    }
  }
}
