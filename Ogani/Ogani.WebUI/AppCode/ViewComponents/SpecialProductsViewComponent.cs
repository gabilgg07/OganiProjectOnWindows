using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Ogani.WebUI.AppCode.Types;
using Ogani.WebUI.Models.DataContext;
using System.Linq;

namespace Ogani.WebUI.AppCode.ViewComponents
{
    public class SpecialProductsViewComponent : ViewComponent
    {
		readonly OganiDbContext db;

        public SpecialProductsViewComponent(OganiDbContext db)
		{
			this.db = db;
		}

		public IViewComponentResult Invoke(string caption, ProductReportType reportType)
		{
			ViewBag.CardTitle = caption;
            var query = db.Products.Where(p => p.DeletedDate == null)
                        .Include(p => p.Images)
                .AsQueryable();

            switch (reportType)
            {
                case ProductReportType.Latest:
                    query = query
                        .OrderByDescending(p => p.Id)
                        .Take(9);
                    break;
                case ProductReportType.Review:
                    query = query
                        .Take(9);
                    break;
                case ProductReportType.TopRated:
                    query = query
                        .Skip(9)
                        .Take(9);
                    break;
                default:
					break;
			}

            var products = query.ToListAsync().Result;

            return View(products);
		}
	}
}

