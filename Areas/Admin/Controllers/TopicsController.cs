using Book_Store.Models;
using Book_Store.Repository.Data;
using Microsoft.AspNetCore.Mvc;

namespace BookStore.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class TopicsController : Controller
    {
        private readonly DataContext _db;

        public TopicsController(DataContext db)
        {
            _db = db;
        }

        public IActionResult Index()
        {
            List<Topic> listThemes = _db.Topics.ToList();
            return View(listThemes);
        }

        public IActionResult Create() => View();

        [HttpPost]
        public IActionResult Create(Topic obj)
        {
            if(ModelState.IsValid)
            {
                _db.Topics.Add(obj);
                _db.SaveChanges();
            }
            return RedirectToAction("Index");
        }
        public IActionResult Edit(int? Id)
        {
            if(Id == null || Id == 0)
            {
                return NotFound();
            }

            Topic? topicFromDB = _db.Topics.Find(Id);

            if(topicFromDB == null)
            {
                return NotFound();
            }

            return View(topicFromDB);
        }

        [HttpPost]
        public IActionResult Edit(Topic obj)
        {
            if (ModelState.IsValid)
            {
                _db.Topics.Update(obj);
                _db.SaveChanges();
            }
            return RedirectToAction("Index");
        }

        public IActionResult Delete(int? Id)
        {
            if(Id == null || Id == 0)
            {
                return NotFound();
            }

            Topic? topicFromDB = _db.Topics.Find(Id);

            if (topicFromDB == null)
            {
                return NotFound();
            }

            return View(topicFromDB);
        }

        [HttpPost, ActionName("Delete")]
        public IActionResult ProcessDelete(int? Id)
        {
            Topic? obj = _db.Topics.Find(Id);
            if (obj == null)
            {
                return NotFound();
            }

            _db.Topics.Remove(obj);
            _db.SaveChanges();

            return RedirectToAction("Index");
        }
    }
}
