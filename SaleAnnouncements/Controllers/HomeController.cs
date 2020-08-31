using Microsoft.AspNetCore.Mvc;
using SaleAnnouncements.BLL.Interfaces;
using SaleAnnouncements.Models;
using System;
using System.Diagnostics;
using System.Threading.Tasks;
using SaleAnnouncements.BLL.Model.Filters;

namespace SaleAnnouncements.Controllers
{
	public class HomeController : Controller
	{
		#region private members

		private readonly IHomePageService _homePageService;
		private readonly ICategoryPageService _categoryPageService;

		#endregion

		#region constructor

		public HomeController(IHomePageService homePageService, 
			ICategoryPageService categoryPageService)
		{
			_homePageService = homePageService ?? throw new ArgumentNullException(nameof(homePageService));
			_categoryPageService = categoryPageService ?? throw new ArgumentNullException(nameof(categoryPageService));
		}

		#endregion

		[ResponseCache(Duration = 300)]
		public async Task<IActionResult> Index()
		{
			var model = await _homePageService.GetPageModel();
			return View(model);
		}

		public IActionResult Privacy()
		{
			return View();
		}

		[ResponseCache(Duration = 300)]
		public async Task<IActionResult> Category(Guid id)
		{
			var model = await _categoryPageService.GetPage(id);
			return View(model);
		}

		[ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
		public IActionResult Error()
		{
			return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
		}
	}
}
