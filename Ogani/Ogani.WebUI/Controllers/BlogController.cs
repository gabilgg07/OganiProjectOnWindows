using System.Linq;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Ogani.WebUI.Models.DataContext;
using Ogani.WebUI.Models.Entity;
using Ogani.WebUI.Models.ViewModel;

namespace Ogani.WebUI.Controllers
{
    [AllowAnonymous]
    public class BlogController : Controller
    {
        readonly OganiDbContext db;

        public BlogController(OganiDbContext db)
        {
            this.db = db;
        }

        public IActionResult Index(int pageIndex = 1, int pageSize = 6, int? categoryId = null, int? tagId = null)
        {

            var query = db.Blogs.AsQueryable();

            if (categoryId != null)
            {
                query = query.Where(b => b.BlogCategoryId == categoryId);
            }
            if (tagId != null)
            {
                query = query.Where(b => b.BlogTagBlogs.Any(btb => btb.BlogTagId == tagId));
            }

            query = query.Where(b => b.DeletedDate == null && b.PublishedDate != null)
                .Include(b => b.Author)
                //.Include(b => b.Comments)
                .AsQueryable();

            var pagedModel = new PagedViewModel<Blog>(query, pageIndex, pageSize,categoryId, tagId);

            return View(pagedModel);
        }

        public IActionResult Details(int id)
        {
            var blog = db.Blogs
                .Include(b => b.BlogCategory)
                .Include(b => b.Author)
                //.Include(b => b.Comments)
                .Include(b => b.BlogTagBlogs)
                .ThenInclude(bt => bt.BlogTag)
                .FirstOrDefault(b => b.Id == id && b.DeletedDate == null);

            if (blog == null)
                return NotFound();

            return View(blog);
        }
    }
}

