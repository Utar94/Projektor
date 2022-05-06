using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Projektor.Core.Repositories;
using Projektor.Infrastructure.Repositories;

namespace Projektor.Infrastructure
{
  public static class ServiceCollectionExtensions
  {
    public static IServiceCollection AddProjektorInfrastructure(this IServiceCollection services)
    {
      return services
        .AddDbContext<ProjektorDbContext>((provider, builder) =>
        {
          var configuration = provider.GetRequiredService<IConfiguration>();
          builder.UseNpgsql(configuration.GetConnectionString(nameof(ProjektorDbContext)));
        })
        .AddScoped<IIssueRepository, IssueRepository>()
        .AddScoped<IIssueTypeRepository, IssueTypeRepository>()
        .AddScoped<IProjectRepository, ProjectRepository>();
    }
  }
}
