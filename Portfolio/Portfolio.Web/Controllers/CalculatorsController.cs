using Microsoft.AspNetCore.Mvc;

namespace Portfolio.Web.Controllers;

public class CalculatorsController : Controller
{
    public IActionResult CostToCompany()
    {
        return View();
    }
}