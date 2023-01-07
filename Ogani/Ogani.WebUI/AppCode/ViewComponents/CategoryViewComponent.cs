using System;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Ogani.WebUI.Models.DataContext;

namespace Ogani.WebUI.AppCode.ViewComponents
{
	public class CategoryViewComponent : ViewComponent
	{
		readonly OganiDbContext db;


        public CategoryViewComponent(OganiDbContext db)
		{
			this.db = db;
		}

		public IViewComponentResult Invoke()
		{
			var categories = db.Categories
				.Where(c => c.DeletedDate == null)
                .ToListAsync().Result;

            return View(categories);
		}
	}
}

