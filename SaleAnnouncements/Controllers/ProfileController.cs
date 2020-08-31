using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SaleAnnouncements.BLL.Interfaces;
using SaleAnnouncements.BLL.Model.Filters;
using System;
using System.Linq;
using System.Threading.Tasks;

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
			var customerId = await _customerService.GetCustomerId(User.Identity.Name!);

			var model = await _offerService.GetForUser(customerId);
			return View(model);
		}

		public IActionResult AddOffer()
		{
			return View();
		}

		public async Task<IActionResult> Messages()
		{
			var currentCustomerId = await _customerService.GetCustomerId(User.Identity.Name!);
			return View(currentCustomerId);
		}

		public async Task<IActionResult> AddStatus(Guid id)
		{
			var model = await _offerService.Get(id);
			return View(model);
		}
	}
}
