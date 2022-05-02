using Microsoft.Extensions.DependencyInjection;

namespace Projektor.Core
{
  public static class ServiceCollectionExtensions
  {
    public static IServiceCollection AddProjektorCore(this IServiceCollection services)
    {
      return services;
    }
  }
}
