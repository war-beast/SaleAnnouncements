using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using SaleAnnouncements.BLL.Dto;
using SaleAnnouncements.BLL.Interfaces;
using SaleAnnouncements.BLL.Model;
using SaleAnnouncements.Models;

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
		private readonly IMessageService _messageService;
		private readonly IPhotoService _photoService;

		#endregion

		#region constructor

		public ApiCommonController(ICategoryService categoryService, 
			IOfferStatusService offerStatusService, 
			IOfferService offerService, 
			IMessageService messageService, 
			IPhotoService photoService)
		{
			_categoryService = categoryService ?? throw new ArgumentNullException(nameof(categoryService));
			_offerStatusService = offerStatusService ?? throw new ArgumentNullException(nameof(offerStatusService));
			_offerService = offerService ?? throw new ArgumentNullException(nameof(offerService));
			_messageService = messageService ?? throw new ArgumentNullException(nameof(messageService));
			_photoService = photoService ?? throw new ArgumentNullException(nameof(photoService));
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

		[HttpGet]
		[Route("getOfferStatuses")]
		public async Task<IActionResult> GetOfferStatuses(Guid id)
		{
			var model = await _offerService.GetOfferStatuses(id);

			return Ok(JsonConvert.SerializeObject(model, Formatting.None, new JsonSerializerSettings
			{
				ContractResolver = new CamelCasePropertyNamesContractResolver()
			}));
		}

		[HttpPost]
		[Route("saveMessage")]
		public async Task<IActionResult> SaveMessage([FromBody] MessageBindingModel model)
		{
			Result result;

			if (!ModelState.IsValid)
			{
				result = new Result
				{
					IsSuccess = false,
					EntityId = Guid.Empty,
					Error = "Ошибка в заполненной форме"
				};

				return BadRequest(JsonConvert.SerializeObject(result, Formatting.None, new JsonSerializerSettings
				{
					ContractResolver = new CamelCasePropertyNamesContractResolver()
				}));
			}

			var messageDto = new MessageDto
			{
				 Description = model.Message,
				 CustomerId = model.CurrentCustomerId,
				 CompanionId = model.OfferOwnerId,
				 Subject = (await _offerService.Get(model.OfferId)).Title
			};

			result = await _messageService.SaveMessage(messageDto);

			return Ok(JsonConvert.SerializeObject(result, Formatting.None, new JsonSerializerSettings
			{
				ContractResolver = new CamelCasePropertyNamesContractResolver()
			}));
		}

		[HttpGet]
		[Route("getPhoto")]
		public async Task<IActionResult> GetPhoto(Guid id)
		{
			var model = await _photoService.Get(id);
			if (model?.Image == null)
				return BadRequest("изображение не найдено");

			return Ok(Convert.ToBase64String(model.Image));
		}
	}
}
