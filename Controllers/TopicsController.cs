using Book_Store.Models;
using Book_Store.Repository.Data;
using Microsoft.AspNetCore.Mvc;

namespace Book_Store.Controllers
{
	public class TopicsController : Controller
	{
		private readonly DataContext _db;
        public TopicsController(DataContext db)
        {
			_db = db;
        }
		public IActionResult Index()
		{
			List<Topic> listTopics = _db.Topics.ToList();
			return View(listTopics);
		}

		public IActionResult BookByTopics(int id) {
			
			var bookByTopics = _db.Books.Where(b => b.TopicId == id).ToList();
			return View(bookByTopics);
		}
	}
}
