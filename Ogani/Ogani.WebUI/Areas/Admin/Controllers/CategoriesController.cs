using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Ogani.WebUI.Models.DataContext;
using Ogani.WebUI.Models.Entity;

namespace Ogani.WebUI.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class CategoriesController : Controller
    {
        readonly OganiDbContext _context;

        public CategoriesController(OganiDbContext context)
        {
            this._context = context;
        }

        [Authorize(Policy = "admin.categories.index")]
        public async Task<IActionResult> Index()
        {
            var categories = await _context.Categories
                .Where(c => c.DeletedDate == null)
                .ToListAsync();
            ViewBag.ToastrMsg = TempData["ToastrMsg"];
            return View(categories);
        }

        [Authorize(Policy = "admin.categories.details")]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var category = await _context.Categories
                .FirstOrDefaultAsync(c => c.Id == id && c.DeletedDate == null);

            if (category == null)
            {
                return NotFound();
            }

            return View(category);
        }

        [Authorize(Policy = "admin.categories.create")]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Policy = "admin.categories.create")]
        public async Task<IActionResult> Create([Bind("Id,Name")] Category category)
        {
            if (ModelState.IsValid)
            {
                _context.Add(category);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(category);
        }

        [Authorize(Policy = "admin.categories.edit")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var category = await _context.Categories.FindAsync(id);

            if (category == null)
            {
                return NotFound();
            }

            return View(category);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Policy = "admin.categories.edit")]
        public async Task<IActionResult> Edit(int id,[Bind("Id,Name")] Category category)
        {
            if (id != category.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(category);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CategoryExists(category.Id))
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
            return View(category);
        }

        // istifade etmirik, sadece numune kimi saxladim:

        //public async Task<IActionResult> Delete(int? id)
        //{
        //    if (id == null)
        //    {
        //        return NotFound();
        //    }

        //    var category = await _context.Categories
        //        .FirstOrDefaultAsync(c => c.Id == id && c.DeletedDate == null);

        //    if (category == null)
        //    {
        //        return NotFound();
        //    }

        //    return View(category);
        //}

        [HttpPost]
        [ValidateAntiForgeryToken] // --> heleki legv edirik bize mane olmasin
        [Authorize(Policy = "admin.categories.delete")]
        public async Task<IActionResult> Delete(int id)
        {
            if (id<=0)
            {
                return Json(new
                {
                    error = true,
                    message = "Xetali muraciet"
                });
            }

            var category = await _context.Categories.FindAsync(id);

            if (category == null)
            {

                return Json(new
                {
                    error = true,
                    message = "Cari qeyd movcud deyil"
                }); 
            }

            category.DeletedDate = DateTime.Now;

            await _context.SaveChangesAsync();

            return Json(new
            {
                error = false,
                message = $"{category.Name}* sistemden silindi!"
            });
        }

        [HttpPost]
        [Authorize(Policy = "admin.categories.delete")]
        public IActionResult ShowToastr( string toastrMsg)
        {
            TempData["ToastrMsg"] = toastrMsg;

            return RedirectToAction(nameof(Index));
        }

        [NonAction]
        private bool CategoryExists(int id)
        {
            return _context.Categories.Any(e => e.Id == id && e.DeletedDate == null);
        }

    }
}

