using Book_Store.Models;
using Book_Store.Repository;
using Book_Store.Repository.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Book_Store.Controllers
{
    public class OrderController : Controller
    {
        private readonly DataContext _db;
		private UserManager<AppCustomer> _userManager;
            
		public OrderController(DataContext db, UserManager<AppCustomer> userManager)
        {
            _db = db;
			_userManager = userManager;

		}

		public async Task<IActionResult> Checkout()
        {
            var userEmail = User.FindFirstValue(ClaimTypes.Email);
            if (userEmail == null)
            {
                return RedirectToAction("SignIn", "Account");
            }
            else
            {
                var Order = new Order();
                //orderItems.DeliveryDate = DateTime.Now;
                Order.OrderDate = DateTime.Now;
                Order.Checkout = 0;
                Order.Status = 1;
                Order.UserName = userEmail;
                AppCustomer cust = await _userManager.FindByEmailAsync(userEmail);
                if (cust == null)
                {
                    return Redirect("/");
				}
                Order.AspNetUserId = cust.Id;
                _db.Add(Order);
				_db.SaveChanges();
                Console.WriteLine(Order);

                List<Cart> cartItems = HttpContext.Session.GetJson<List<Cart>>("Cart") ?? new List<Cart>();
                foreach(var items in cartItems)
                {
                    var orderDetail = new OrderDetail();
                    orderDetail.OrderId = Order.Id;
                    orderDetail.BookId = items.BookId;
                    orderDetail.Price = items.Price;
                    orderDetail.Quantity = items.Quantity;

                    _db.Add(orderDetail);
                    _db.SaveChanges();
                }
                HttpContext.Session.Remove("Cart");

                //_db.Add(orderItems);
                //_db.SaveChanges();

                return Redirect("/");
            }
        }
    }
}
