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
		private readonly IOfferService _offerService;

		#endregion

		#region constructor

		public HomePageService(ICategoryService categoryService, 
			IOfferService offerService)
		{
			_categoryService = categoryService ?? throw new ArgumentNullException(nameof(categoryService));
			_offerService = offerService ?? throw new ArgumentNullException(nameof(offerService));
		}

		#endregion

		public async Task<HomeViewModel> GetPageModel()
		{
			var categories = await _categoryService.GetAll();
			categories = await GetCategoriesWithOffers(categories)
				.ToListAsync();

			return new HomeViewModel
			{
				Categories = categories.OrderBy(x => x.Name).ToList()
			};
		}

		#region private methods

		private async IAsyncEnumerable<CategoryDto> GetCategoriesWithOffers(IReadOnlyCollection<CategoryDto> categories)
		{
			foreach (var category in categories)
			{
				var offers = await _offerService.GetByCategory(category.Id!.Value);
				category.Offers = offers.Select(x => new OfferDto
				{
					Id = x.Id,
					Title = x.Title
				});

				yield return category;
			}
		}

		#endregion
	}
}
