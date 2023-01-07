using System;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Ogani.WebUI.Models.DataContext;

namespace Ogani.WebUI.AppCode.ViewComponents
{
	public class RecentNewsViewComponent : ViewComponent
    {
        readonly OganiDbContext db;

        public RecentNewsViewComponent(OganiDbContext db)
        {
            this.db = db;
        }

        public IViewComponentResult Invoke()
        {
            var lastBlogs = db.Blogs
                .Where(b => b.DeletedDate == null)
                .OrderByDescending(b => b.Id)
                .Take(3)
                .ToListAsync().Result;

            return View(lastBlogs);
        }
    }
}

