using Logitar.WebApiToolKit;

namespace Projektor.Web
{
  public class Startup : StartupBase
  {
    private readonly ConfigurationOptions _options = new();
    private readonly IConfiguration _configuration;

    public Startup(IConfiguration configuration)
    {
      _configuration = configuration;
    }

    public override void ConfigureServices(IServiceCollection services)
    {
      base.ConfigureServices(services);

      services.AddWebApiToolKit(_configuration, _options);
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
