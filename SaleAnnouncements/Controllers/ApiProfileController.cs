using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using SaleAnnouncements.BLL.Model;
using SaleAnnouncements.Models;

namespace SaleAnnouncements.Controllers
{
	[Route("api/profile")]
	[ApiController]
	[Authorize]
	public class ApiProfileController : ControllerBase
	{
		#region private members



		#endregion

		#region constructor

		public ApiProfileController()
		{

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

			result = new Result
			{
				IsSuccess = false,
				EntityId = Guid.Empty
			};

			return Ok(JsonConvert.SerializeObject(result, Formatting.None, new JsonSerializerSettings
			{
				ContractResolver = new CamelCasePropertyNamesContractResolver()
			}));
		}
	}
}
