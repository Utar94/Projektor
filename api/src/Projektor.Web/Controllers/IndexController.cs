using Logitar.WebApiToolKit.Configuration;
using Microsoft.AspNetCore.Mvc;

namespace Projektor.Web.Controllers
{
  [ApiExplorerSettings(IgnoreApi = true)]
  [Route("")]
  public class IndexController : ControllerBase
  {
    private readonly ApiSettings _apiSettings;

    public IndexController(ApiSettings apiSettings)
    {
      _apiSettings = apiSettings;
    }

    public IActionResult Get() => Ok(_apiSettings.Name);
  }
}
