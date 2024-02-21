using Microsoft.AspNetCore.Mvc;

namespace Book_Store.Controllers
{
	public class BlogController : Controller
	{
		public IActionResult Index() => View();
		
	}
}
