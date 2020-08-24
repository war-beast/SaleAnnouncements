using System;
using System.Collections.Generic;

namespace SaleAnnouncements.BLL.Dto
{
	public class CategoryDto
	{
		public Guid Id { get; set; }

		public string Name { get; set; } = string.Empty;

		public IEnumerable<OfferDto> Offers { get; set; } = new List<OfferDto>();
	}
}
