using Microsoft.AspNetCore.Mvc;
using SaleAnnouncements.BLL.Interfaces;
using System;
using System.Threading.Tasks;

namespace SaleAnnouncements.Controllers
{
	public class CommonController : Controller
	{
		#region private members

		private readonly IPhotoService _photoService;

		#endregion

		#region constructor

		public CommonController(IPhotoService photoService)
		{
			_photoService = photoService ?? throw new ArgumentNullException(nameof(photoService));
		}

		#endregion

		public async Task<IActionResult> GetPhoto(Guid id)
		{
			var model = await _photoService.Get(id);
			return PartialView("_PhotModal", model.Image);
		}
	}
}
