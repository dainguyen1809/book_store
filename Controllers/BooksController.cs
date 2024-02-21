using Book_Store.Models;
using Book_Store.Models.Dtos;
using Book_Store.Repository.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Book_Store.Controllers
{
	public class BooksController : Controller
	{
		private readonly DataContext _db;
        public BooksController(DataContext db)
        {
            _db = db;
        }

		[Route("/books")]
		public IActionResult Index()
		{
			List<Book> listBooks = _db.Books.ToList();
			return View(listBooks);
		}

		public IActionResult Details(int id)
		{
			var books = _db.Books.Include(b => b.Publisher).FirstOrDefault(b => b.Id == id);

			if(books == null)
			{
				return NotFound();
			}

			return View(books);
		}

        public IActionResult BookByPublisher(int id)
        {
            var bookByPublisher = _db.Books.Where(b => b.PublisherId == id).ToList();
            return View(bookByPublisher);
        }

		[Route("/books/ShowBookAPI")]
		public async Task<IActionResult> ShowBookAPI()
		{
			using (var httpClient = new HttpClient())
			{
				string apiUrl = "https://localhost:7245/api/BookAPI/"; 

				try
				{
					HttpResponseMessage response = await httpClient.GetAsync(apiUrl);

					if (response.IsSuccessStatusCode)
					{
						// API call was successful
						string responseData = await response.Content.ReadAsStringAsync();
						List<Book> books = Newtonsoft.Json.JsonConvert.DeserializeObject<List<Book>>(responseData);
						return View(books);
					}
					else
					{
						// Handle unsuccessful API call
						return Redirect("/");
					}
				}
				catch (Exception ex)
				{
					// Handle exception
					Console.WriteLine("An error occurred: " + ex.Message);
					return Redirect("");
				}
			}
		}
	}
}
