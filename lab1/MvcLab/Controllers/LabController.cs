using Microsoft.AspNetCore.Mvc;

namespace LabMvcProject.Controllers;

public class LabController : Controller
{
    public IActionResult Info()
    {
        var labInfo = new
        {
            LabNumber = 1,
            Topic = "Розробка ASP.NET (Development with ASP.NET)",
            Goal = "Створення проектів та робота з middleware",
            StudentName = "Вєщиков Олег" 
        };

        return View(labInfo);
    }
}