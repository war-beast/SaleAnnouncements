using System;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SaleAnnouncements.BLL.Interfaces;
using SaleAnnouncements.BLL.Model.Filters;

namespace SaleAnnouncements.Controllers
{
	[Authorize]
	public class ProfileController : Controller
	{
		#region private members

		private readonly IMapper _mapper;
		private readonly IOfferService _offerService;
		private readonly ICustomerService _customerService;

		#endregion

		#region constructor

		public ProfileController(IMapper mapper, 
			IOfferService offerService, 
			ICustomerService customerService)
		{
			_offerService = offerService ?? throw new ArgumentNullException(nameof(offerService));
			_customerService = customerService ?? throw new ArgumentNullException(nameof(offerService));
			_mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
		}

		#endregion

		public async Task<IActionResult> Index()
		{
			var customersFilter = new CustomerFilterBuilder()
				.SetEmail(User.Identity.Name!)
				.Build();
			var customers = await _customerService.GetFiltered(customersFilter);

			var model = await _offerService.GetForUser(customers.First().Id!.Value);
			return View(model);
		}

		public IActionResult AddOffer()
		{
			return View();
		}

		public IActionResult Messages()
		{
			return View();
		}

		public async Task<IActionResult> AddStatus(Guid id)
		{
			var model = await _offerService.Get(id);
			return View(model);
		}
	}
}
