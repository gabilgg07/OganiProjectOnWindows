using System;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Ogani.WebUI.AppCode.Extensions;
using Ogani.WebUI.Models.DataContext;
using Ogani.WebUI.Models.Entity;

namespace Ogani.WebUI.Controllers
{
    [AllowAnonymous]
    public class HomeController : Controller
    {
        readonly OganiDbContext db;

        public HomeController(OganiDbContext db)
        {
            this.db = db;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Contact()
        {
            Task.Delay(2000).Wait();
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Contact(ContactPost cPost)
        {
            if (ModelState.IsValid)
            {
                db.ContactPosts.Add(cPost);
                db.SaveChanges();

                ViewBag.State = Tuple.Create("Sorgunuz qebul edildi, qisa muddetde geri donus edeceyik",false);
                ModelState.Clear();
                return View();
            }

            ViewBag.State = Tuple.Create("Zehmet olmasa melumatlarin dogrulugunu yeniden yoxlayasiniz", true);
            return View(cPost);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Subscribe(string email)
        {
            if (!email.IsEmail())
            {
                return Json(new
                {
                    error = true,
                    message = $"'{email}' - email formati ucun uygun deyil!"
                });
            }

            var founded = db.Subscribes.FirstOrDefault(s => s.Email.Equals(email));

            if (founded != null)
            {

                return Json(new
                {
                    error = false,
                    message = $"'{email}' - email artiq qeydiyatdan kecib"
                });
            }

            var subscribe = new Subscribe
            {
                Email = email
            };

            db.Subscribes.Add(subscribe);

            db.SaveChanges();

            string token = $"{subscribe.Id}-{subscribe.Email}".Encrypt();

            var callbackUrl = Url.Action("ConfirmSubscribe", "Home", new
            {
                token = token
            }, protocol: Request.Scheme);

            var sendState = email.SendEmail($"Zehmet olmasa abunelik isteyinizi <a href='{callbackUrl.ToString()}'>testiq edin!</a>");

            if (!sendState)
            {
                db.Subscribes.Remove(subscribe);
                db.SaveChanges();

                return Json(new
                {
                    error = true,
                    message = $"Muveqqeti olaraq xidmet islemir. Zehmet olmasa biraz sonra yeniden cehd edin"
                });
            }


            var anonymousObj = new
            {
                error = false,
                message = "Siz yeniliklere abune olma sorgusu gonderdiniz, " +
                "E-poct unvaniniza gonderilen linki testiqlemekle emeliyyati tamamlaya bilersiniz",
                email = email
            };

            return Json(anonymousObj);
        }

        public IActionResult ConfirmSubscribe(string token)
        {
            try
            {
                token = token.Decrypt();
            }
            catch (Exception)
            {
                return View(Tuple.Create("Xetali muraciet. Acar deaktivdir", true));
            }


            Match match = Regex.Match(token, @"^(?<id>\d+)-(?<email>([\w-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([\w-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?))$");

            if (!match.Success)
            {
                return View(Tuple.Create("Xetali muraciet. Acar deaktivdir", true));
            }

            int id = Convert.ToInt32(match.Groups["id"].Value);
            string email = match.Groups["email"].Value;

            var founded = db.Subscribes.FirstOrDefault(s => s.Id == id && s.ConfirmedDate == null);

            if (founded == null)
            {
                return View(Tuple.Create("Xetali muraciet. Acar deaktivdir", true));
            }

            if (!founded.Email.Equals(email))
            {
                return View(Tuple.Create("Xetali muraciet. Acar deaktivdir", true));
            }

            founded.ConfirmedDate = DateTime.Now;
            db.SaveChanges();

            return View(Tuple.Create("Tebrikler siz yeniliklere abune oldunuz", false));
        }
    }
}

