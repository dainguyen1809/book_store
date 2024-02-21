using Book_Store.Models;
using Book_Store.Repository.Data;
using Microsoft.AspNetCore.Mvc;

namespace BookStore.Areas.Admin.Controllers
{
    [Area("Admin")]
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

        public IActionResult Create() => View();

        [HttpPost]
        public IActionResult Create(Publisher obj)
        {
            if (ModelState.IsValid)
            {
                _db.Publishers.Add(obj);
                _db.SaveChanges();
            }
            return View();
        }

        public IActionResult Edit(int? Id)
        {
            if(Id == null || Id == 0)
            {
                return NotFound();
            }

            Publisher? publisherFromDB = _db.Publishers.Find(Id);

            if (publisherFromDB == null)
            {
                return NotFound();
            }

            return View(publisherFromDB);
        }

        [HttpPost]
        public IActionResult Edit(Publisher obj)
        {
            if (ModelState.IsValid)
            {
                _db.Publishers.Update(obj);
                _db.SaveChanges();
            }
            return RedirectToAction("Index");
        }

        public IActionResult Delete(int? Id)
        {
            if (Id == null || Id == 0)
            {
                return NotFound();
            }

            Publisher? publisherFromDB = _db.Publishers.Find(Id);

            if (publisherFromDB == null)
            {
                return NotFound();
            }

            return View(publisherFromDB);
        }

        [HttpPost, ActionName("Delete")]
        public IActionResult ProcessDelete(int? Id)
        {
            Publisher? obj = _db.Publishers.Find(Id);

            if(obj == null)
            {
                return NotFound();
            }

            _db.Publishers.Remove(obj);
            _db.SaveChanges();
            
            return RedirectToAction("Index");
        }

    }
}
