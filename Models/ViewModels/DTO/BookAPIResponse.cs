using Book_Store.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Book_Store.Models.Dtos
{
	public class BookResponse
	{
		public int Count { get; set; }
		public string Next { get; set; }
		public string Previous { get; set; }
		public List<BookAPI> Results { get; set; }
	}
}