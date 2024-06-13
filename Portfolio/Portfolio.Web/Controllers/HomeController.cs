using Microsoft.AspNetCore.Mvc;
using Portfolio.Web.Models;
using System.Diagnostics;
using System.Text;

namespace Portfolio.Web.Controllers
{
    public class HomeController : ControllerBase
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

		public IActionResult GenerateGarbage()
		{
			string garbageData = GenerateRandomString(4000);
			return View((object)garbageData);
		}

		static string GenerateRandomString(int length)
		{
			var random = new Random();
			const string chars = "ABCDEGHIJKLMNOPQRSTVWXYZabcdeghijklmnopqrstvwxyz";
			StringBuilder stringBuilder = new(length);

			for (int i = 0; i < length; i++)
			{
				stringBuilder.Append(chars[random.Next(chars.Length)]);
			}

			return stringBuilder.ToString();
		}
	}
}