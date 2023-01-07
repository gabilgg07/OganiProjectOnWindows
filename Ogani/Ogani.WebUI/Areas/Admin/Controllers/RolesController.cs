using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Ogani.WebUI.Models.DataContext;
using Ogani.WebUI.Models.Entity.Membership;

namespace Ogani.WebUI.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class RolesController : MainController
    {
        private readonly OganiDbContext _context;
        private readonly RoleManager<OganiRole> roleManager;
        public RolesController(OganiDbContext context, RoleManager<OganiRole> roleManager)
        {
            _context = context;
            this.roleManager = roleManager;
        }

        [Authorize(Policy = "admin.roles.index")]
        public async Task<IActionResult> Index()
        {
            ViewBag.ToastrMsg = TempData["ToastrMsg"];
            return View(await _context.Roles.Where(r => r.DeletedDate == null).ToListAsync());
        }

        [Authorize(Policy = "admin.roles.details")]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || id < 1)
            {
                return NotFound();
            }

            var role = await _context.Roles
                .FirstOrDefaultAsync(m => m.Id == id);
            if (role == null)
            {
                return NotFound();
            }

            var users = (from u in _context.Users
                         join ur in _context.UserRoles on new
                         {
                             RoleId = id ?? 0,
                             UserId = u.Id
                         } equals new { ur.RoleId, ur.UserId }
                        into lGroup
                         from lg in lGroup.DefaultIfEmpty()
                         select Tuple.Create(u.Id, u.UserName, lg != null ? true : false))
                        .ToArray();

            ViewBag.Users = users;

            if (Program.policies != null)
            {

                var aviablePolicies = (from policy in Program.policies
                                       join rc in _context.RoleClaims
                                       on new { RoleId = id??0, ClaimType = policy, ClaimValue = "1" }
                                       equals new { rc.RoleId, rc.ClaimType, rc.ClaimValue } into lGroup
                                       from lg in lGroup.DefaultIfEmpty()
                                       select Tuple.Create(policy, lg != null)).ToArray();

                ViewBag.Policies = aviablePolicies;
            }

            return View(role);
        }

        [Authorize(Policy = "admin.roles.create")]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Policy = "admin.roles.create")]
        public async Task<IActionResult> Create([Bind("Name")] OganiRole role)
        {
            if (ModelState.IsValid)
            {
                var hasRole = roleManager.RoleExistsAsync(role.Name).Result;

                if (hasRole)
                {
                    ModelState.AddModelError("Name", $"{role.Name} adli role artiq movcuddur");
                    return View(role);
                }

                await roleManager.CreateAsync(new OganiRole
                {
                    Name = role.Name
                });

                return RedirectToAction(nameof(Index));
            }
            return View(role);
        }

        [Authorize(Policy = "admin.roles.edit")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var role = await _context.Roles.FindAsync(id);
            if (role == null)
            {
                return NotFound();
            }
            return View(role);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Policy = "admin.roles.edit")]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name")] OganiRole role)
        {
            if (id != role.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    var roleFound = await roleManager.FindByIdAsync(id.ToString());

                    if (roleFound == null)
                    {
                        return NotFound();
                    }

                    var roleFoundByModelName = await roleManager.FindByNameAsync(role.Name);

                    if (roleFound.Name != role.Name && roleFoundByModelName != null)
                    {

                        ModelState.AddModelError("Name", $"{role.Name} adli role artiq movcuddur");
                        return View(role);
                    }

                    await roleManager.SetRoleNameAsync(roleFound,role.Name);

                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!OganiRoleExists(role.Id))
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
            return View(role);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Policy = "admin.roles.delete")]
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

            var role = await _context.Roles.FirstOrDefaultAsync(r => r.Id == id);

            if (role == null)
            {

                return Json(new
                {
                    error = true,
                    message = "Cari qeyd movcud deyil"
                });
            }

            role.DeletedDate = DateTime.Now;

            await _context.SaveChangesAsync();

            return Json(new
            {
                error = false,
                message = $"{role.Name}* sistemden silindi!"
            });
        }

        [HttpPost]
        [Authorize(Policy = "admin.roles.delete")]
        public IActionResult ShowToastr(string toastrMsg)
        {
            TempData["ToastrMsg"] = toastrMsg;

            return RedirectToAction(nameof(Index));
        }

        private bool OganiRoleExists(int id)
        {
            return _context.Roles.Any(e => e.Id == id);
        }


        [HttpPost]
        [Authorize(Policy = "admin.roles.setrole")]
        public async Task<IActionResult> SetRole(int roleId, int userId, bool selected)
        {
            var role = await _context.Roles.FirstOrDefaultAsync(r => r.Id == roleId);

            if (role == null)
            {
                return Json(new
                {
                    error = true,
                    message = "Bele role yoxdur"
                });
            }

            var user = await _context.Users.FirstOrDefaultAsync(u => u.Id == userId);

            if (user == null)
            {
                return Json(new
                {
                    error = true,
                    message = "Istifadeci tapilmadi"
                });
            }

            if (selected)
            {

                if (await _context.UserRoles.AnyAsync(ur => ur.UserId == userId && ur.RoleId == roleId))
                {
                    return Json(new
                    {
                        error = true,
                        message = "Istifadeci bu roldadir"
                    });
                }

                await _context.UserRoles.AddAsync(new OganiUserRole
                {
                    UserId = userId,
                    RoleId = roleId
                });

                await _context.SaveChangesAsync();
            }
            else
            {

                if (userId == UserId)
                {
                    return Json(new
                    {
                        error = true,
                        message = "Istifadeci oz rolunu deyise bilmez"
                    });
                }

                var userRole = await _context.UserRoles.FirstOrDefaultAsync
                    (ur => ur.UserId == userId && ur.RoleId == roleId);
                if (userRole == null)
                {
                    return Json(new
                    {
                        error = true,
                        message = "Istifadeci bu rolda deyil"
                    });
                }

                _context.UserRoles.Remove(userRole);
                await _context.SaveChangesAsync();
            }

            return Json(new
            {
                error = false,
                message = "Successed"
            });
        }

        [HttpPost]
        [Authorize(Policy = "admin.roles.setpolicy")]
        public async Task<IActionResult> SetPolicy(int roleId, string policyName, bool selected)
        {
            var role = await _context.Roles.FirstOrDefaultAsync(r => r.Id == roleId);

            if (role == null)
            {
                return Json(new
                {
                    error = true,
                    message = "Bele role tapilmadi"
                });
            }

            if (Program.policies == null || !Program.policies.Contains(policyName))
            {
                return Json(new
                {
                    error = true,
                    message = "Bele selahiyyet yoxdur"
                });
            }

            if (selected)
            {

                if (_context.RoleClaims.Any(rc => rc.RoleId == roleId &&
                rc.ClaimType.Equals(policyName) &&
                rc.ClaimValue.Equals("1")))
                {
                    return Json(new
                    {
                        error = true,
                        message = "Bu rola bu selahiyyet verilib"
                    });
                }

                await _context.RoleClaims.AddAsync(new OganiRoleClaim
                {
                    RoleId = roleId,
                    ClaimType = policyName,
                    ClaimValue = "1"
                });

                await _context.SaveChangesAsync();
            }
            else
            {

                var claim = await _context.RoleClaims.FirstOrDefaultAsync(rc =>
                rc.RoleId == roleId &&
                rc.ClaimType.Equals(policyName) &&
                rc.ClaimValue.Equals("1"));

                if (claim == null)
                {
                    return Json(new
                    {
                        error = true,
                        message = "Role uzre silinecek selahiyyet movcud deyil"
                    });
                }

                _context.RoleClaims.Remove(claim);
                await _context.SaveChangesAsync();
            }

            return Json(new
            {
                error = false,
                message = "Successed"
            });
        }

    }
}
