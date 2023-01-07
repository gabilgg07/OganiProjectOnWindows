using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Ogani.WebUI.Models.DataContext;
using Ogani.WebUI.Models.Entity;

namespace Ogani.WebUI.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class AuthorsController : Controller
    {
        private readonly OganiDbContext _context;
        private readonly IWebHostEnvironment _env;

        public AuthorsController(OganiDbContext context, IWebHostEnvironment env)
        {
            _context = context;
            _env = env;
        }

        [Authorize(Policy = "admin.authors.index")]
        public async Task<IActionResult> Index()
        {
            var models = await _context.Authors
                .Where(c => c.DeletedDate == null)
                .ToListAsync();
            ViewBag.ToastrMsg = TempData["ToastrMsg"];
            return View(models);
        }

        [Authorize(Policy = "admin.authors.details")]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var model = await _context.Authors
                .FirstOrDefaultAsync(m => m.Id == id && m.DeletedDate == null);
            if (model == null)
            {
                return NotFound();
            }

            return View(model);
        }

        [Authorize(Policy = "admin.authors.create")]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Policy = "admin.authors.create")]
        public async Task<IActionResult> Create([Bind("Id,FullName,Role,Image")] Author model)
        {
            if (model.Image == null)
            {
                ModelState.AddModelError("Image", "Sekil gonderilmeyib");
            }

            if (ModelState.IsValid)
            {
                string extension = Path.GetExtension(model.Image.FileName);
                string pureName = $"{DateTime.Now.ToString("yyMMddHHmmssfff")}-{Guid.NewGuid()}{extension}";
                string fullPath = Path.Combine(_env.WebRootPath, "uploads", "images", "authors", pureName);

                using (var stream = new FileStream(fullPath, FileMode.Create, FileAccess.Write))
                {
                    model.Image.CopyTo(stream);
                }

                model.ImagePath = pureName;


                _context.Add(model);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(model);
        }

        [Authorize(Policy = "admin.authors.edit")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var model = await _context.Authors.FindAsync(id);
            if (model == null)
            {
                return NotFound();
            }
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Policy = "admin.authors.edit")]
        public async Task<IActionResult> Edit(int id, [Bind("Id,FullName,Role,Image,ImagePath")] Author model)
        {
            if (id != model.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                var entity = await _context.Authors
                    .FirstOrDefaultAsync(m => m.Id == model.Id);

                if (entity == null)
                    return NotFound();

                try
                {
                    if (model.Image == null && string.IsNullOrWhiteSpace(model.ImagePath))
                    {
                        if (!string.IsNullOrWhiteSpace(entity.ImagePath))
                        {
                            entity.ImagePath = Path.Combine(_env.WebRootPath,
                                "uploads",
                                "images",
                                "authors", entity.ImagePath);

                            if (System.IO.File.Exists(entity.ImagePath))
                                System.IO.File.Delete(entity.ImagePath);

                            entity.ImagePath = null;
                        }

                    }
                    else if (model.Image != null)
                    {
                        if (!string.IsNullOrWhiteSpace(entity.ImagePath))
                        {
                            entity.ImagePath = Path.Combine(_env.WebRootPath,
                                "uploads",
                                "images",
                                "authors", entity.ImagePath);

                            if (System.IO.File.Exists(entity.ImagePath))
                                System.IO.File.Delete(entity.ImagePath);

                        }

                        string extension = Path.GetExtension(model.Image.FileName);
                        string pureName = $"{DateTime.Now.ToString("yyMMddHHmmssfff")}-{Guid.NewGuid()}{extension}";
                        entity.ImagePath = Path.Combine(_env.WebRootPath, "uploads", "images", "authors", pureName);

                        using (var stream = new FileStream(entity.ImagePath, FileMode.Create, FileAccess.Write))
                            model.Image.CopyTo(stream);

                        entity.ImagePath = pureName;
                    }

                    entity.FullName = model.FullName;
                    entity.Role = model.Role;

                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AuthorExists(model.Id))
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
        [Authorize(Policy = "admin.authors.delete")]
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

            var model = await _context.Authors.FindAsync(id);

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
                message = $"{model.FullName}* sistemden silindi!"
            });
        }

        [HttpPost]
        [Authorize(Policy = "admin.authors.delete")]
        public IActionResult ShowToastr(string toastrMsg)
        {
            TempData["ToastrMsg"] = toastrMsg;

            return RedirectToAction(nameof(Index));
        }

        [NonAction]
        private bool AuthorExists(int id)
        {
            return _context.Authors.Any(e => e.Id == id);
        }
    }
}
