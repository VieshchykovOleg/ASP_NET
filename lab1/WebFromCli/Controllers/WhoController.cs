using Microsoft.AspNetCore.Mvc;

namespace WebFromCli.Controllers;

[ApiController]
[Route("[controller]")]
public class WhoController : ControllerBase
{
    [HttpGet]
    public string Get()
    {
        return "Олег Вєщиков";
    }
}
