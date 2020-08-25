using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace SaleAnnouncements.Controllers
{
	[Authorize]
	public class ProfileController : Controller
	{
		public IActionResult Index()
		{
			return View();
		}

		public IActionResult AddOffer()
		{
			return View();
		}

		public IActionResult SendMessage()
		{
			return View();
		}
	}
}
