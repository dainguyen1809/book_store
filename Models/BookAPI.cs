namespace Book_Store.Models
{
	public class BookAPI
	{
		public int Id { get; set; }
		public string Title { get; set; }
		public List<Author> Authors { get; set; }
		public List<string> Subjects { get; set; }
		public List<string> Bookshelves { get; set; }
		public List<string> Languages { get; set; }
		public bool Copyright { get; set; }
		public string MediaType { get; set; }
		public Dictionary<string, string> Formats { get; set; }
		public int DownloadCount { get; set; }
	}
}
