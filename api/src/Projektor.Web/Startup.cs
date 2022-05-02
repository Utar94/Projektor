namespace Projektor.Web
{
  public class Startup : StartupBase
  {
    private readonly IConfiguration _configuration;

    public Startup(IConfiguration configuration)
    {
      _configuration = configuration;
    }

    public override void ConfigureServices(IServiceCollection services)
    {
      base.ConfigureServices(services);

      services.AddControllers();
      services.AddEndpointsApiExplorer();
      services.AddSwaggerGen();
    }

    public override void Configure(IApplicationBuilder applicationBuilder)
    {
      if (applicationBuilder is WebApplication application)
      {
        if (application.Environment.IsDevelopment())
        {
          application.UseSwagger();
          application.UseSwaggerUI();
        }

        application.UseHttpsRedirection();
        application.UseAuthorization();
        application.MapControllers();
      }
    }
  }
}
