using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Ogani.WebUI.Models.DataContext;
using Ogani.WebUI.Models.Entity;

namespace Ogani.WebUI.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class BlogsController : Controller
    {
        private readonly OganiDbContext _context;
        private readonly IWebHostEnvironment _env;

        public BlogsController(OganiDbContext context, IWebHostEnvironment env)
        {
            _context = context;
            _env = env;
        }

        [Authorize(Policy = "admin.blogs.index")]
        public async Task<IActionResult> Index()
        {
            var oganiDbContext = _context.Blogs
                .Where(b => b.DeletedDate == null)
                .Include(b => b.Author)
                .Include(b => b.BlogCategory);
            ViewBag.ToastrMsg = TempData["ToastrMsg"];
            return View(await oganiDbContext.ToListAsync());
        }

        [Authorize(Policy = "admin.blogs.details")]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var blog = await _context.Blogs
                .Include(b => b.Author)
                .Include(b => b.BlogCategory)
                .FirstOrDefaultAsync(m => m.Id == id && m.DeletedDate == null);
            if (blog == null)
            {
                return NotFound();
            }

            return View(blog);
        }

        [Authorize(Policy = "admin.blogs.create")]
        public IActionResult Create()
        {
            ViewData["AuthorId"] = new SelectList(_context.Authors, "Id", "FullName");
            ViewData["BlogCategoryId"] = new SelectList(_context.BlogCategories, "Id", "Name");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Policy = "admin.blogs.create")]
        public async Task<IActionResult> Create([Bind("Title,Body,Image,Facebook,Twitter,Linkedin,Instagram,AuthorId,BlogCategoryId")] Blog blog)
        {
            if (blog.Image == null)
            {
                ModelState.AddModelError("Image","Sekil gonderilmeyib");
            }

            if (ModelState.IsValid)
            {
                string extension = Path.GetExtension(blog.Image.FileName);
                string pureName = $"{DateTime.Now.ToString("yyMMddHHmmssfff")}-{Guid.NewGuid()}{extension}";
                string fullPath = Path.Combine(_env.WebRootPath, "uploads", "images", "blogs", pureName);

                using(var stream = new FileStream(fullPath, FileMode.Create, FileAccess.Write))
                {
                    blog.Image.CopyTo(stream);
                }

                blog.ImagePath = pureName;

                _context.Add(blog);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["AuthorId"] = new SelectList(_context.Authors, "Id", "FullName", blog.AuthorId);
            ViewData["BlogCategoryId"] = new SelectList(_context.BlogCategories, "Id", "Name", blog.BlogCategoryId);
            return View(blog);
        }

        [Authorize(Policy = "admin.blogs.edit")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var blog = await _context.Blogs.FindAsync(id);
            if (blog == null)
            {
                return NotFound();
            }
            ViewData["AuthorId"] = new SelectList(_context.Authors, "Id", "FullName", blog.AuthorId);
            ViewData["BlogCategoryId"] = new SelectList(_context.BlogCategories, "Id", "Name", blog.BlogCategoryId);
            return View(blog);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Policy = "admin.blogs.edit")]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Title,Body,Image,ImagePath,Facebook,Twitter,Linkedin,Instagram,AuthorId,BlogCategoryId")] Blog blog)
        {

            if (id != blog.Id)
                return NotFound();

            if (ModelState.IsValid)
            {
                var entity = await _context.Blogs
                    .FirstOrDefaultAsync(b => b.Id == blog.Id);

                if (entity == null)
                    return NotFound();

                try
                {
                    if (blog.Image == null && string.IsNullOrWhiteSpace(blog.ImagePath))
                    {
                        // sekil yoxdusa, ve ya silibse, kohneni sil:

                        if (!string.IsNullOrWhiteSpace(entity.ImagePath))
                        {
                            entity.ImagePath = Path.Combine(_env.WebRootPath,
                                "uploads",
                                "images",
                                "blogs", entity.ImagePath);

                            if (System.IO.File.Exists(entity.ImagePath))
                                System.IO.File.Delete(entity.ImagePath);

                            entity.ImagePath = null;
                        }

                    }
                    else if(blog.Image != null)
                    {
                        // teze sekil varsa, kohneni sil:

                        if (!string.IsNullOrWhiteSpace(entity.ImagePath))
                        {
                            entity.ImagePath = Path.Combine(_env.WebRootPath,
                                "uploads",
                                "images",
                                "blogs", entity.ImagePath);

                            if (System.IO.File.Exists(entity.ImagePath))
                                System.IO.File.Delete(entity.ImagePath);

                        }

                        string extension = Path.GetExtension(blog.Image.FileName);
                        string pureName = $"{DateTime.Now.ToString("yyMMddHHmmssfff")}-{Guid.NewGuid()}{extension}";
                        entity.ImagePath = Path.Combine(_env.WebRootPath, "uploads", "images", "blogs", pureName);

                        using (var stream = new FileStream(entity.ImagePath, FileMode.Create, FileAccess.Write))
                            blog.Image.CopyTo(stream);

                        entity.ImagePath = pureName;
                    }

                    entity.Title = blog.Title;
                    entity.Body = blog.Body;
                    entity.AuthorId = blog.AuthorId;
                    entity.BlogCategoryId = blog.BlogCategoryId;
                    entity.Facebook = blog.Facebook;
                    entity.Twitter = blog.Twitter;
                    entity.Linkedin = blog.Linkedin;
                    entity.Instagram = blog.Instagram;
                    entity.PublishedDate = blog.PublishedDate;

                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!BlogExists(blog.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["AuthorId"] = new SelectList(_context.Authors, "Id", "FullName", blog.AuthorId);
            ViewData["BlogCategoryId"] = new SelectList(_context.BlogCategories, "Id", "Name", blog.BlogCategoryId);
            return View(blog);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Policy = "admin.blogs.delete")]
        public async Task<IActionResult> Delete(int id)
        {
            if (id <= 0)
            {
                return Json(new
                {
                    error = true,
                    message = "Xetali muraciet"
                });
            }

            var blog = await _context.Blogs.FindAsync(id);

            if (blog == null)
            {

                return Json(new
                {
                    error = true,
                    message = "Cari qeyd movcud deyil"
                });
            }

            blog.DeletedDate = DateTime.Now;

            await _context.SaveChangesAsync();

            return Json(new
            {
                error = false,
                message = $"{blog.Title}* sistemden silindi!"
            });
        }

        [HttpPost]
        [Authorize(Policy = "admin.blogs.delete")]
        public IActionResult ShowToastr(string toastrMsg)
        {
            TempData["ToastrMsg"] = toastrMsg;

            return RedirectToAction(nameof(Index));
        }

        [NonAction]
        private bool BlogExists(int id)
        {
            return _context.Blogs.Any(e => e.Id == id);
        }
    }
}
