using Book_Store.Models.Dtos;
using Book_Store.Models;
using Microsoft.AspNetCore.Mvc;

namespace Book_Store.Controllers
{
	public class PublicBookAPI : Controller
	{
		public IActionResult Index()
		{
			string apiUrl = "https://gutendex.com/books";

			using (HttpClient client = new HttpClient())
			{
				try
				{
					string jsonData = GetJsonData(apiUrl);
					List<BookAPI> books = Newtonsoft.Json.JsonConvert.DeserializeObject<BookResponse>(jsonData)?.Results;
					return View(books);
				}
				catch (Exception ex)
				{
					Console.WriteLine($"Exception: {ex.Message}");
					return Redirect("/Shop");

				}
			}
		}

		static string GetJsonData(string apiUrl)
		{
			using (HttpClient client = new HttpClient())
			{
				HttpResponseMessage response = client.GetAsync(apiUrl).Result;

				if (response.IsSuccessStatusCode)
				{
					return response.Content.ReadAsStringAsync().Result;
				}
				else
				{
					throw new HttpRequestException($"Failed to retrieve data. Status code: {response.StatusCode}");
				}
			}
		}
	}
}

