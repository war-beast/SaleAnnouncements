using System.Collections.Generic;

namespace SaleAnnouncements.BLL.Dto
{
	public class CustomerDto
	{
		public string Id { get; set; } = string.Empty;

		public string UserName { get; set; } = string.Empty;

		public string Email { get; set; } = string.Empty;

		public bool EmailConfirmed { get; set; } = false;

		public IEnumerable<OfferDto> SalesOffers { get; set; } = new List<OfferDto>();

		public IEnumerable<MessageDto> Messages { get; set; } = new List<MessageDto>();
	}
}
