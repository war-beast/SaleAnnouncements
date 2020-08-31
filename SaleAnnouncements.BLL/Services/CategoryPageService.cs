using SaleAnnouncements.BLL.Interfaces;
using SaleAnnouncements.BLL.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SaleAnnouncements.BLL.Dto;
using SaleAnnouncements.BLL.Model.Filters;

namespace SaleAnnouncements.BLL.Services
{
	public class CategoryPageService : ICategoryPageService
	{
		#region private members

		private readonly ICategoryService _categoryService;
		private readonly IOfferService _offerService;
		private readonly ICustomerService _customerService;

		#endregion

		#region constructor

		public CategoryPageService(ICategoryService categoryService,
			IOfferService offerService,
			ICustomerService customerService)
		{
			_categoryService = categoryService ?? throw new ArgumentNullException(nameof(categoryService));
			_offerService = offerService ?? throw new ArgumentNullException(nameof(offerService));
			_customerService = customerService ?? throw new ArgumentNullException(nameof(customerService));
		}

		#endregion

		public async Task<CategoryViewModel> GetPage(Guid id, string? currentCustomerName = null)
		{
			var category = await _categoryService.Get(id);
			IReadOnlyCollection<CustomerDto> customers = new List<CustomerDto>();

			if (string.IsNullOrWhiteSpace(currentCustomerName))
			{
				var customersFilter = new CustomerFilterBuilder()
					.SetEmail(currentCustomerName!)
					.Build();
				customers = await _customerService.GetFiltered(customersFilter);
			}

			var result = new CategoryViewModel
			{
				Offers = await _offerService.GetByCategory(category.Id!.Value),
				Title = category.Name
			};

			if (customers.Count > 0)
			{
				result.CurrentCustomerId = customers.First().Id;
			}

			return result;
		}
	}
}
