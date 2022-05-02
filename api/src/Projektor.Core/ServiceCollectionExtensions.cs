using MediatR;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace Projektor.Core
{
  public static class ServiceCollectionExtensions
  {
    public static IServiceCollection AddProjektorCore(this IServiceCollection services)
    {
      var assembly = Assembly.GetExecutingAssembly();

      return services
        .AddAutoMapper(assembly)
        .AddMediatR(assembly);
    }
  }
}
