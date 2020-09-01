using SaleAnnouncements.BLL.Interfaces;
using SaleAnnouncements.BLL.Model;
using SaleAnnouncements.DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using SaleAnnouncements.BLL.Dto;
using SaleAnnouncements.BLL.Model.Filters;

namespace SaleAnnouncements.BLL.Services
{
	public class SearchService : ServiceBase, ISearchService
	{
		#region private members

		private readonly IMapper _mapper;
		private readonly ICustomerService _customerService;

		#endregion

		#region constructor

		public SearchService(IUnitOfWork unitOfWork, 
			IMapper mapper, 
			ICustomerService customerService) : base(unitOfWork)
		{
			_mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
			_customerService = customerService ?? throw new ArgumentNullException(nameof(customerService));
		}

		#endregion

		public async Task<SearchResultsViewModel> GetSearchResults(string phrase, string? userName = null)
		{
			#region validation

			if(string.IsNullOrWhiteSpace(phrase))
				throw new ArgumentNullException(nameof(phrase));

			#endregion

			var offers = _unitOfWork.Offers
				.GetAll()
				.Where(x => x.Title.Contains(phrase))
				.OrderByDescending(x => x.Sort)
				.ThenByDescending(x => x.UpdateDate);

			var offersDto = await Task.Run(() => _mapper.Map<List<OfferDto>>(offers));

			IReadOnlyCollection<CustomerDto> customers = new List<CustomerDto>();
			if (!string.IsNullOrWhiteSpace(userName))
			{
				var customersFilter = new CustomerFilterBuilder()
					.SetEmail(userName!)
					.Build();
				customers = await _customerService.GetFiltered(customersFilter);
			}

			var result = new SearchResultsViewModel
			{
				Title = phrase,
				Offers = offersDto
			}; 
			
			if (customers.Count > 0)
			{
				result.CurrentCustomerId = customers.First().Id;
			}

			return result;
		}
	}
}
