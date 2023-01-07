using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Ogani.WebUI.Models.DataContext;
using Ogani.WebUI.Models.Entity;

namespace Ogani.WebUI.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class BlogTagsController : Controller
    {
        private readonly OganiDbContext _context;

        public BlogTagsController(OganiDbContext context)
        {
            _context = context;
        }

        [Authorize(Policy = "admin.blogTags.index")]
        public async Task<IActionResult> Index()
        {
            var models = await _context.BlogTags
                .Where(c => c.DeletedDate == null)
                .ToListAsync();
            ViewBag.ToastrMsg = TempData["ToastrMsg"];
            return View(models);
        }

        [Authorize(Policy = "admin.blogTags.details")]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var model = await _context.BlogTags
                .FirstOrDefaultAsync(m => m.Id == id && m.DeletedDate == null);
            if (model == null)
            {
                return NotFound();
            }

            return View(model);
        }

        [Authorize(Policy = "admin.blogTags.create")]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Policy = "admin.blogTags.create")]
        public async Task<IActionResult> Create([Bind("Id,Name")] BlogTag model)
        {
            if (ModelState.IsValid)
            {
                _context.Add(model);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(model);
        }

        [Authorize(Policy = "admin.blogTags.edit")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var model = await _context.BlogTags.FindAsync(id);
            if (model == null)
            {
                return NotFound();
            }
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Policy = "admin.blogTags.edit")]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name")] BlogTag model)
        {
            if (id != model.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(model);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!BlogTagExists(model.Id))
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
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Policy = "admin.blogTags.delete")]
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

            var model = await _context.BlogTags.FindAsync(id);

            if (model == null)
            {

                return Json(new
                {
                    error = true,
                    message = "Cari qeyd movcud deyil"
                });
            }

            model.DeletedDate = DateTime.Now;

            await _context.SaveChangesAsync();

            return Json(new
            {
                error = false,
                message = $"{model.Name}* sistemden silindi!"
            });
        }


        [HttpPost]
        [Authorize(Policy = "admin.blogTags.delete")]
        public IActionResult ShowToastr(string toastrMsg)
        {
            TempData["ToastrMsg"] = toastrMsg;

            return RedirectToAction(nameof(Index));
        }

        [NonAction]
        private bool BlogTagExists(int id)
        {
            return _context.BlogTags.Any(e => e.Id == id);
        }
    }
}
