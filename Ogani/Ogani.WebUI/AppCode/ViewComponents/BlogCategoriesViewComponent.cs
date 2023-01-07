using System;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using Ogani.WebUI.Models.DataContext;
using Microsoft.EntityFrameworkCore;

namespace Ogani.WebUI.AppCode.ViewComponents
{
    public class BlogCategoriesViewComponent : ViewComponent
    {
        readonly OganiDbContext db;

        public BlogCategoriesViewComponent(OganiDbContext db)
        {
            this.db = db;
        }

        public IViewComponentResult Invoke()
        {
            var blogsCategories = db.BlogCategories
                .Where(bc => bc.DeletedDate == null)
                .ToListAsync().Result;

            return View(blogsCategories);
        }
    }
}

