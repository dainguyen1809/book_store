using Book_Store.Repository.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Book_Store.Repository.Components
{
	public class ListTopicsViewComponent : ViewComponent
	{
		private readonly DataContext _db;
		public ListTopicsViewComponent(DataContext db)
		{
			_db = db;
		}

		public async Task<IViewComponentResult> InvokeAsync() => View(await _db.Topics.ToListAsync());
	}
}
