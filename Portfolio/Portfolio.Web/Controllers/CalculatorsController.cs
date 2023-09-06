using Microsoft.AspNetCore.Mvc;

namespace Portfolio.Web.Controllers;

public class CalculatorsController : ControllerBase
{
    public IActionResult CostToCompany()
    {
        return View();
    }
}