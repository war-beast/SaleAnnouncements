using SaleAnnouncements.BLL.Interfaces;
using SaleAnnouncements.BLL.Model;
using System;
using System.Threading.Tasks;

namespace SaleAnnouncements.BLL.Services
{
	public class CategoryPageService : ICategoryPageService
	{
		#region private members

		private readonly ICategoryService _categoryService;
		private readonly IOfferService _offerService;

		#endregion

		#region constructor

		public CategoryPageService(ICategoryService categoryService, 
			IOfferService offerService)
		{
			_categoryService = categoryService ?? throw new ArgumentNullException(nameof(categoryService));
			_offerService = offerService ?? throw new ArgumentNullException(nameof(offerService));
		}

		#endregion

		public async Task<CategoryViewModel> GetPage(Guid id)
		{
			var category = await _categoryService.Get(id);

			return new CategoryViewModel
			{
				Offers = await _offerService.GetByCategory(category.Id!.Value),
				Title = category.Name
			};
		}
	}
}
