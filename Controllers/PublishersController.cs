using Book_Store.Models;
using Book_Store.Repository.Data;
using Microsoft.AspNetCore.Mvc;

namespace Book_Store.Controllers
{
    public class PublishersController : Controller
    {
        private readonly DataContext _db;
        public PublishersController(DataContext db)
        {
            _db = db;
        }

        public IActionResult Index()
        {
            List<Publisher> listPublishers = _db.Publishers.ToList();
            return View(listPublishers);
        }

        public IActionResult ListPublishers()
        {
            List<Publisher> listPublishers = _db.Publishers.ToList();
            return View(listPublishers);
        }
    }
}
