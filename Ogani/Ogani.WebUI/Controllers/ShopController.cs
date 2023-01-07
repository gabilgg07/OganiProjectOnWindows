using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using Ogani.WebUI.Models.DataContext;
using Ogani.WebUI.Models.Entity;
using Ogani.WebUI.Models.ViewModel;

namespace Ogani.WebUI.Controllers
{
    [AllowAnonymous]
    public class ShopController : Controller
    {
        readonly OganiDbContext db;

        public ShopController(OganiDbContext db)
        {
            this.db = db;
        }

        public IActionResult Index(int? categoryId,int pageIndex = 1, int pageSize = 3)
        {
            // lazy loading
            //List<Product> products = db.Products.ToList();

            //List<Product> products = db.Products
            //    .Include(p => p.Images)
            //    .Skip((pageIndex-1)*pageSize)
            //    .Take(pageSize)
            //    .ToList();
            var query = db.Products.AsQueryable();

            if (categoryId != null)
            {
                query = query.Where(p => p.CategoryId == categoryId);
            }

            query = query
                .Where(p => p.DeletedDate == null)
                .Include(p => p.Images);

            var pagedModel = new PagedViewModel<Product>(query, pageIndex, pageSize,categoryId);

            return View(pagedModel);
        }

        public IActionResult Details(int id)
        {
            var product = db.Products
                .Include(p => p.Images)
                .Include(p => p.Unit)
                .FirstOrDefault(p => p.Id == id && p.DeletedDate == null);

            return View(product);
        }

        public async Task<IActionResult> ShoppingCard()
        {
            if (Request.Cookies.TryGetValue("basket", out string basketJson))
            {
                var basket = JsonConvert.DeserializeObject<List<BasketItem>>(basketJson);

                foreach (var basketItem in basket)
                {
                    var product = await db.Products
                        .Include(p => p.Images)
                        .FirstOrDefaultAsync(p => p.Id == basketItem.ProductId && p.DeletedDate == null);

                    basketItem.Name = product.Name;
                    basketItem.Price = product.Price;
                    basketItem.ImagePath = product.Images?
                        .FirstOrDefault(i => i.IsMain)?.ImagePath;
                }

                return View(basket);
            }
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> ShoppingCardAmount()
        {
            // method base linq:

            //var dataLinq = db.Products.Where(p => p.CategoryId == 1).ToList();

            /*

            select p.*, c.[Name] from [dbo].[Products] p
            join dbo.Categories c on p.CategoryId = c.Id
            where p.CategoryId = 1
             
             */

            //var data = (from p in db.Products
            //            join c in db.Categories on p.CategoryId equals c.Id
            //            where p.CategoryId == 1
            //            select new
            //            {
            //                Product = p,
            //                Category = c
            //            }).ToList();


            if (Request.Cookies.TryGetValue("basket", out string basketJson))
            {
                var basket = JsonConvert.DeserializeObject<List<BasketItem>>(basketJson);


                int[] productIds = basket.Select(b => b.ProductId).ToArray();

                var basketProducts = await db.Products.Where(p => productIds.Contains(p.Id)).ToListAsync();

                var amountsSum = (from p in basketProducts
                                 join b in basket on p.Id equals b.ProductId
                                 select p.Price * b.Count)
                                 .Sum(a => a);

                //foreach (var basketItem in basket)
                //{
                //    var product = await db.Products
                //        .FirstOrDefaultAsync(p => p.Id == basketItem.ProductId && p.DeletedDate == null);

                //    basketItem.Price = product.Price;
                //}

                return Json(new
                {
                    error = false,
                    //amount = basket.Sum(b => b.Amount),
                    amount = amountsSum,
                    message = ""
                });
            }
            return Json(new
            {
                error = true,
                message = "Cookie bosdur"
            });
        }

        public async Task<IActionResult> Checkout()
        {
            if (Request.Cookies.TryGetValue("basket", out string basketJson))
            {
                var basket = JsonConvert.DeserializeObject<List<BasketItem>>(basketJson);

                foreach (var basketItem in basket)
                {
                    var product = await db.Products
                        .FirstOrDefaultAsync(p => p.Id == basketItem.ProductId && p.DeletedDate == null);

                    basketItem.Name = product.Name;
                    basketItem.Price = product.Price;
                }

                return View(basket);
            }
            return View();
        }
    }
}

