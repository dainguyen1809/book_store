using Microsoft.AspNetCore.Mvc;

namespace Book_Store.Controllers
{
	public class AuthController : Controller
	{
		public IActionResult Index()
		{
			return View();
		}
	}
}
