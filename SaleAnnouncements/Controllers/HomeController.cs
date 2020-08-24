using System;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using SaleAnnouncements.Models;
using System.Diagnostics;
using System.Threading.Tasks;
using SaleAnnouncements.BLL.Interfaces;

namespace SaleAnnouncements.Controllers
{
	public class HomeController : Controller
	{
		#region private members

		private readonly ILogger<HomeController> _logger;
		private readonly IHomePageService _homePageService;

		#endregion

		#region constructor

		public HomeController(ILogger<HomeController> logger, IHomePageService homePageService)
		{
			_logger = logger ?? throw new ArgumentNullException(nameof(logger));
			_homePageService = homePageService ?? throw new ArgumentNullException(nameof(homePageService));
		}

		#endregion


		public async Task<IActionResult> Index()
		{
			var model = await _homePageService.GetPageModel();
			return View(model);
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
