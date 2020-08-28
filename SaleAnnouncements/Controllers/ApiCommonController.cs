using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using SaleAnnouncements.BLL.Interfaces;

namespace SaleAnnouncements.Controllers
{
	[Route("api/common")]
	[ApiController]
	public class ApiCommonController : ControllerBase
	{
		#region private members

		private readonly ICategoryService _categoryService;
		private readonly IOfferStatusService _offerStatusService;
		private readonly IOfferService _offerService;

		#endregion

		#region constructor

		public ApiCommonController(ICategoryService categoryService, 
			IOfferStatusService offerStatusService, 
			IOfferService offerService)
		{
			_categoryService = categoryService ?? throw new ArgumentNullException(nameof(categoryService));
			_offerStatusService = offerStatusService ?? throw new ArgumentNullException(nameof(offerStatusService));
			_offerService = offerService ?? throw new ArgumentNullException(nameof(offerService));
		}

		#endregion

		[HttpGet]
		[Route("getCategories")]
		public async Task<IActionResult> GetCategories()
		{
			var result = await _categoryService.GetAll();

			return Ok(JsonConvert.SerializeObject(result, Formatting.None, new JsonSerializerSettings
			{
				ContractResolver = new CamelCasePropertyNamesContractResolver()
			}));
		}

		[HttpGet]
		[Route("getStatuses")]
		public async Task<IActionResult> GetStatuses()
		{
			var result = await _offerStatusService.GetAll();

			return Ok(JsonConvert.SerializeObject(result, Formatting.None, new JsonSerializerSettings
			{
				ContractResolver = new CamelCasePropertyNamesContractResolver()
			}));
		}

		[HttpGet]
		[Route("getOfferPhoneNumber")]
		[Authorize]
		public async Task<ContentResult> GetOfferPhoneNumber(Guid id)
		{
			var offer = await _offerService.Get(id);

			return Content(offer.PhoneNumber);
		}
	}
}
