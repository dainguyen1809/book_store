using Book_Store.Models;
using Book_Store.Repository.Data;
using Microsoft.AspNetCore.Mvc;

namespace Book_Store.Controllers
{
	[Route("api/BookAPI")]
	[ApiController]
	public class BookAPIController : ControllerBase
	{
		private readonly DataContext _db;
		private readonly IHttpClientFactory _httpClientFactory;
		public BookAPIController(DataContext db, IHttpClientFactory httpClientFactory)
        {
            _db = db;
			_httpClientFactory = httpClientFactory;
        }

		[HttpGet]
		public IEnumerable<Book> GetBooks()
		{
			List<Book> listBooks = _db.Books.ToList();

			return listBooks;
		}

		[HttpGet("{id:int}")]
		public IActionResult GetBookByID(int id)
		{
			Book? book = _db.Books.Where(x => x.Id == id).FirstOrDefault();

			return Ok(book);
		}
    }
}
