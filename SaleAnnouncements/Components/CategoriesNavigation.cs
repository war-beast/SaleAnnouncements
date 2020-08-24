using Microsoft.AspNetCore.Mvc;
using SaleAnnouncements.BLL.Interfaces;
using System;
using System.Threading.Tasks;

namespace SaleAnnouncements.Components
{
	public class CategoriesNavigation : ViewComponent
	{
		#region private members

		private readonly ICategoryService _categoryService;

		#endregion

		#region constructor

		public CategoriesNavigation(ICategoryService categoryService)
		{
			_categoryService = categoryService ?? throw new ArgumentNullException(nameof(categoryService));
		}

		#endregion

		public async Task<IViewComponentResult> InvokeAsync()
		{
			var categories = await _categoryService.GetAll();

			return View("CategoriesNavigation", categories);
		}
	}
}
