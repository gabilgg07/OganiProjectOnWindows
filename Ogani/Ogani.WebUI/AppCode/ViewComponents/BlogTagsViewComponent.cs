using System;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Ogani.WebUI.Models.DataContext;

namespace Ogani.WebUI.AppCode.ViewComponents
{
	public class BlogTagsViewComponent : ViewComponent
    {
        readonly OganiDbContext db;

        public BlogTagsViewComponent(OganiDbContext db)
        {
            this.db = db;
        }

        public IViewComponentResult Invoke()
        {
            var blogTags = db.BlogTags
                .Where(bt => bt.DeletedDate == null)
                .ToListAsync().Result;

            return View(blogTags);
        }
    }
}

