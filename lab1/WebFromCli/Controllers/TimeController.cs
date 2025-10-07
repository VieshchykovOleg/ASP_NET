using Microsoft.AspNetCore.Mvc;

namespace WebFromCli.Controllers;

[ApiController]
[Route("[controller]")]
public class TimeController : ControllerBase
{
    [HttpGet]
    public string Get()
    {
        return DateTime.Now.ToString("HH:mm:ss");
    }
}
