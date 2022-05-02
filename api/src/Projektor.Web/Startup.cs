using Logitar.AspNetCore.Identity;
using Logitar.Identity.EntityFrameworkCore;
using Logitar.WebApiToolKit;
using Projektor.Core;
using Projektor.Infrastructure;

namespace Projektor.Web
{
  public class Startup : StartupBase
  {
    private readonly ConfigurationOptions _options = new();
    private readonly IConfiguration _configuration;

    public Startup(IConfiguration configuration)
    {
      _configuration = configuration;

      _options.Filters.Add<IdentityExceptionFilterAttribute>();
    }

    public override void ConfigureServices(IServiceCollection services)
    {
      base.ConfigureServices(services);

      services.AddDefaultIdentity(_configuration)
        .WithEntityFrameworkStores<ProjektorDbContext>();

      services.AddWebApiToolKit(_configuration, _options);

      services.AddProjektorCore();
      services.AddProjektorInfrastructure();
    }

    public override void Configure(IApplicationBuilder applicationBuilder)
    {
      if (applicationBuilder is WebApplication application)
      {
        application.UseWebApiToolKit(_options);
      }
    }
  }
}
