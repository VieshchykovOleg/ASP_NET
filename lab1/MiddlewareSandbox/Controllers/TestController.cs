using Microsoft.AspNetCore.Mvc;

namespace MiddlewareSandbox.Controllers;

[ApiController]
[Route("api/[controller]")]
public class TestController : ControllerBase
{
    [HttpGet]
    public IActionResult Get()
    {
        return Ok(new { message = "GET request successful", timestamp = DateTime.Now });
    }

    [HttpPost]
    public IActionResult Post([FromBody] object data)
    {
        return Ok(new { message = "POST request successful", receivedData = data });
    }

    [HttpPut]
    public IActionResult Put([FromBody] object data)
    {
        return Ok(new { message = "PUT request successful", receivedData = data });
    }

    [HttpDelete]
    public IActionResult Delete()
    {
        return Ok(new { message = "DELETE request successful" });
    }
}

[ApiController]
[Route("api/[controller]")]
public class UserController : ControllerBase
{
    [HttpGet]
    public IActionResult GetUsers()
    {
        return Ok(new { message = "Getting all users" });
    }

    [HttpGet("{id}")]
    public IActionResult GetUser(int id)
    {
        return Ok(new { message = $"Getting user with id {id}" });
    }
}

[ApiController]
[Route("api/[controller]")]
public class ProductController : ControllerBase
{
    [HttpGet]
    public IActionResult GetProducts()
    {
        return Ok(new { message = "Getting all products" });
    }
}