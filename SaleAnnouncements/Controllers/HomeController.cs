using Microsoft.AspNetCore.Mvc;
using SaleAnnouncements.BLL.Interfaces;
using SaleAnnouncements.Models;
using System;
using System.Diagnostics;
using System.Threading.Tasks;
using SaleAnnouncements.BLL.Model;

namespace SaleAnnouncements.Controllers
{
	public class HomeController : Controller
	{
		#region private members

		private readonly IHomePageService _homePageService;
		private readonly ICategoryPageService _categoryPageService;
		private readonly ISearchService _searchService;

		#endregion

		#region constructor

		public HomeController(IHomePageService homePageService, 
			ICategoryPageService categoryPageService, 
			ISearchService searchService)
		{
			_homePageService = homePageService ?? throw new ArgumentNullException(nameof(homePageService));
			_categoryPageService = categoryPageService ?? throw new ArgumentNullException(nameof(categoryPageService));
			_searchService = searchService ?? throw new ArgumentNullException(nameof(categoryPageService));
		}

		#endregion

		[ResponseCache(CacheProfileName = "Default")]
		public async Task<IActionResult> Index()
		{
			var model = await _homePageService.GetPageModel();
			return View(model);
		}

		public IActionResult Privacy()
		{
			return View();
		}

		[ResponseCache(CacheProfileName = "Default")]
		public async Task<IActionResult> Category(Guid id)
		{
			var model = await _categoryPageService.GetPage(id, User.Identity.Name);
			return View(model);
		}

		[ResponseCache(CacheProfileName = "Default", VaryByQueryKeys = new[] {"phrase"})]
		public async Task<IActionResult> Search(string phrase)
		{
			#region validation

			if (string.IsNullOrWhiteSpace(phrase))
			{
				return View(new SearchResultsViewModel
				{
					Title = "",
					CurrentCustomerId = Guid.Empty
				});
			}

			#endregion

			var model = await _searchService.GetSearchResults(phrase, User.Identity.Name);
			return View(model);
		}

		[ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
		public IActionResult Error()
		{
			return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
		}
	}
}
