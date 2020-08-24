using SaleAnnouncements.BLL.Dto;
using System.Collections.Generic;

namespace SaleAnnouncements.BLL.Model
{
	public class HomeViewModel
	{
		public IReadOnlyCollection<CategoryDto> Categories { get; set; } = null!;

		public IReadOnlyCollection<OfferDto> Offers { get; set; } = null!;
	}
}
