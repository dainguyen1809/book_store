using Book_Store.Models;
using Book_Store.Repository;
using Book_Store.Repository.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BookStore.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class HomeController : Controller
    {
        private UserManager<AppCustomer> _userManager;
        private SignInManager<AppCustomer> _signInManager;
        public HomeController(SignInManager<AppCustomer> signInManager, UserManager<AppCustomer> userManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }
        public async Task<IActionResult> Index()
        {
            string? userId = HttpContext.Session.GetJson<string>("UserId");

            var user = await _userManager.FindByIdAsync(userId);
            if(user == null)
            {
                return NotFound();
            }
            if (user.Role != "Admin")
            {
                return Redirect("/");
            }

            return View();
        }
	}
}
