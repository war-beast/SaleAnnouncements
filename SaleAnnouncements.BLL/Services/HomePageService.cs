using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SaleAnnouncements.BLL.Dto;
using SaleAnnouncements.BLL.Interfaces;
using SaleAnnouncements.BLL.Model;

namespace SaleAnnouncements.BLL.Services
{
	public class HomePageService : IHomePageService
	{
		#region private members

		private readonly ICategoryService _categoryService;

		#endregion

		#region constructor

		public HomePageService(ICategoryService categoryService)
		{
			_categoryService = categoryService ?? throw new ArgumentNullException(nameof(categoryService));
		}

		#endregion

		public async Task<HomePageModel> GetPageModel()
		{
			var categories = await _categoryService.GetAll();

			return new HomePageModel
			{
				Categories = categories.OrderBy(x => x.Name).ToList(),
				Offers = new List<OfferDto>()
			};
		}
	}
}
