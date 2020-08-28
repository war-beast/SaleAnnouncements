using SaleAnnouncements.BLL.Dto;
using System.Collections.Generic;

namespace SaleAnnouncements.BLL.Model
{
	public class CategoryViewModel
	{
		public string Title { get; set; } = string.Empty;

		public IReadOnlyCollection<OfferDto> Offers { get; set; } = new List<OfferDto>();
	}
}
