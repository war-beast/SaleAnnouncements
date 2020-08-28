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

		#endregion

		#region constructor

		public ApiProfileController(IOfferService offerService, 
			IMapper mapper, 
			IPhotoService photoService)
		{
			_offerService = offerService ?? throw new ArgumentNullException(nameof(offerService));
			_mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
			_photoService = photoService ?? throw new ArgumentNullException(nameof(photoService));
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
	}
}
