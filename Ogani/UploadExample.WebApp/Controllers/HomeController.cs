using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using UploadExample.WebApp.Models;

namespace UploadExample.WebApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IWebHostEnvironment _env;

        public HomeController(ILogger<HomeController> logger, IWebHostEnvironment env)
        {
            _logger = logger;
            _env = env;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Index(string name, IFormFile profilePhoto)
        {
            if (profilePhoto == null)
            {
                ModelState.AddModelError("profilePhoto", "Sekil gonderilmeyib");
            }

            string extension = Path.GetExtension(profilePhoto.FileName);

            string pureName = $"{DateTime.Now.ToString("yyMMddHHmmss")}-{Guid.NewGuid()}{extension}";

            string fullPath = Path.Combine(_env.WebRootPath,"uploads","images", pureName);

            using(var stream = new FileStream(fullPath, FileMode.Create, FileAccess.Write))
            {
                profilePhoto.CopyTo(stream);
            }

            return View(Tuple.Create(name, pureName));
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}

