using Book_Store.Models;
using Book_Store.Repository.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace BookStore.Areas.Admin.Controllers
{
	[Area("Admin")]
    public class BooksController : Controller
    {
        private readonly DataContext _db;
        public BooksController(DataContext db)
        {
            _db = db;
        }

		public async Task<IActionResult> Index() => View(await _db.Books.OrderBy(b => b.Id).Include(b => b.Topic).Include(b => b.Publisher).ToListAsync());

        public IActionResult Create()
		{
			ViewBag.Topics = new SelectList(_db.Topics, "Id", "TopicName");
			ViewBag.Publishers = new SelectList(_db.Publishers, "Id", "NamePublisher");
            return View();
		}

		[HttpPost]
		public IActionResult Create(Book obj, IFormFile UrlImgCover)
		{
			try 
			{
				if (UrlImgCover != null && UrlImgCover.Length > 0)
				{
					var imgPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/Images", UrlImgCover.FileName);
					using (var stream = new FileStream(imgPath, FileMode.Create))
					{
                        UrlImgCover.CopyTo(stream);
					}
					obj.UrlImgCover =  UrlImgCover.FileName;
				}
				_db.Books.Add(obj);
				_db.SaveChanges();
			return RedirectToAction("Index");
			}
			catch(Exception e){
                Console.WriteLine(e);
            }
			return View();
		}

        public async Task<IActionResult> Edit(int? id)
        {
			if (id == null || id == 0)
			{
				return NotFound();
			}

			Book? BookFromDB = await _db.Books.FindAsync(id);

			ViewBag.Topics = new SelectList(_db.Topics, "Id", "TopicName");
			ViewBag.Publishers = new SelectList(_db.Publishers, "Id", "NamePublisher");

			if(BookFromDB == null )
			{
				return NotFound();
			}

			return View(BookFromDB);
        }



        [HttpPost]
        public IActionResult Edit(Book obj, IFormFile UrlImgCover)
        {
            try
            {
                // Lấy thông tin sách từ database
                var existingBook = _db.Books.AsNoTracking().FirstOrDefault(b => b.Id == obj.Id);

                // Kiểm tra xem có ảnh cũ không và xóa nó
                if (!string.IsNullOrEmpty(existingBook?.UrlImgCover))
                {
                    var oldImagePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/Images", existingBook.UrlImgCover);
                    if (System.IO.File.Exists(oldImagePath))
                    {
                        System.IO.File.Delete(oldImagePath);
                    }
                }

                // lưu ảnh mới 
                if (UrlImgCover != null && UrlImgCover.Length > 0)
                {
                    var imgPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/Images", UrlImgCover.FileName);
                    using (var stream = new FileStream(imgPath, FileMode.Create))
                    {
                        UrlImgCover.CopyTo(stream);
                    }
                    obj.UrlImgCover = UrlImgCover.FileName;
                }

                // Cập nhật thông tin sách
                _db.Books.Update(obj);
                _db.SaveChanges();
                return RedirectToAction("Index");
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
            return View();
        }

        public IActionResult Delete(int? id)
        {

            if (id == null || id == 0)
            {
                return NotFound();
            }

            Book? BookFromDB =  _db.Books.Find(id);

            if(BookFromDB == null)
            {
                return NotFound();
            }

            // Xóa ảnh từ wwwroot/Images
            if (!string.IsNullOrEmpty(BookFromDB.UrlImgCover))
            {
                var imagePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/Images", BookFromDB.UrlImgCover);
                if (System.IO.File.Exists(imagePath))
                {
                    System.IO.File.Delete(imagePath);
                }
            }

            _db.Books.Remove(BookFromDB);
            _db.SaveChanges();

            return RedirectToAction("Index");

        }


    }
}
