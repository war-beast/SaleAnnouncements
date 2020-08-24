using System;
using System.Collections.Generic;

namespace SaleAnnouncements.BLL.Dto
{
	public class CategoryDto : DtoBase
	{
		public string Name { get; set; } = string.Empty;

		public IEnumerable<OfferDto> Offers { get; set; } = new List<OfferDto>();
	}
}
