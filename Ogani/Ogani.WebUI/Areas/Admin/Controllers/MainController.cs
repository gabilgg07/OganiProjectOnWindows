using System;
using System.Linq;
using System.Security.Claims;
using Microsoft.AspNetCore.Mvc;

namespace Ogani.WebUI.Areas.Admin.Controllers
{
	public class MainController : Controller
	{
		public int UserId
		{
			get
			{
				return Convert.ToInt32(User.Claims.FirstOrDefault(c => c.Type.Equals(ClaimTypes.NameIdentifier))?.Value ?? "0");
			}
		}
	}
}

