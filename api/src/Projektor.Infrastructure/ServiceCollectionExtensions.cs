using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

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
        });
    }
  }
}
