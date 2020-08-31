using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using SaleAnnouncements.BLL.Dto;
using SaleAnnouncements.BLL.Interfaces;
using SaleAnnouncements.BLL.Model;
using SaleAnnouncements.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.Internal;

namespace SaleAnnouncements.Controllers
{
	[Route("api/profile")]
	[ApiController]
	[Authorize]
	public class ApiProfileController : ControllerBase
	{
		#region private members

		private readonly IMapper _mapper;
		private readonly IOfferService _offerService;
		private readonly IPhotoService _photoService;
		private readonly ICustomerService _customerService;
		private readonly IMessageService _messageService;

		#endregion

		#region constructor

		public ApiProfileController(IOfferService offerService, 
			IMapper mapper, 
			IPhotoService photoService, 
			ICustomerService customerService, 
			IMessageService messageService)
		{
			_offerService = offerService ?? throw new ArgumentNullException(nameof(offerService));
			_mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
			_photoService = photoService ?? throw new ArgumentNullException(nameof(photoService));
			_customerService = customerService ?? throw new ArgumentNullException(nameof(customerService));
			_messageService = messageService ?? throw new ArgumentNullException(nameof(messageService));
		}

		#endregion

		[HttpPost]
		[Route("addOffer")]
		public async Task<IActionResult> AddOffer([FromForm] OfferBindingModel model)
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

			result = await _offerService.Create(_mapper.Map<OfferDto>(model), User.Identity.Name!);
			if (result.IsSuccess)
			{
				var offerPhotos = new List<PhotoDto>();
				foreach (var photo in model.Photos)
				{
					if (photo != null)
					{
						byte[] imageData = null;
						using var binaryReader = new BinaryReader(photo.OpenReadStream());
						imageData = binaryReader.ReadBytes((int)photo.Length);

						offerPhotos.Add(new PhotoDto
						{
							Image = imageData,
							OfferId = result.EntityId
						});
					}
				}

				if (offerPhotos.Count > 0)
				{
					result = await _photoService.Save(offerPhotos, result.EntityId);
				}
			}

			return Ok(JsonConvert.SerializeObject(result, Formatting.None, new JsonSerializerSettings
			{
				ContractResolver = new CamelCasePropertyNamesContractResolver()
			}));
		}

		[HttpPost]
		[Route("addStatus")]
		public async Task<IActionResult> AddStatus([FromBody] NewStatusBindingModel model)
		{
			Result result;

			if (!ModelState.IsValid || !model.SelectedStatusIds.Any())
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

			result = await _offerService.AddStatuses(model.Id, model.SelectedStatusIds);

			return Ok(JsonConvert.SerializeObject(result, Formatting.None, new JsonSerializerSettings
			{
				ContractResolver = new CamelCasePropertyNamesContractResolver()
			}));
		}

		[HttpGet]
		[Route("getCustomerMessages")]
		public async Task<IActionResult> GetCustomerMessages(Guid id)
		{
			var currentCustomerId = await _customerService.GetCustomerId(User.Identity.Name!);
			if (id != currentCustomerId)
			{
				return BadRequest("Получен запрос от постороннего пользователя");
			}

			var result = await _messageService.GetUserMessageTitles(id);

			return Ok(JsonConvert.SerializeObject(result, Formatting.None, new JsonSerializerSettings
			{
				ContractResolver = new CamelCasePropertyNamesContractResolver()
			}));
		}
	}
}
