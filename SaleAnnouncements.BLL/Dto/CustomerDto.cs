using System;
using System.Collections.Generic;

namespace SaleAnnouncements.BLL.Dto
{
	public class CustomerDto : DtoBase
	{
		public string UserId { get; set; } = string.Empty;

		public string UserName { get; set; } = string.Empty;

		public string UserEmail { get; set; } = string.Empty;

		public IEnumerable<OfferDto> SalesOffers { get; set; } = new List<OfferDto>();

		public IEnumerable<MessageDto> Messages { get; set; } = new List<MessageDto>();
	}
}
