using System;

namespace SaleAnnouncements.BLL.Dto
{
	public class OfferStatusMapDto : DtoBase
	{
		public Guid OfferId { get; set; }

		public Guid StatusId { get; set; }

		public OfferDto Offer { get; set; } = new OfferDto();

		public OfferStatusDto Status { get; set; } = new OfferStatusDto();

		public DateTime CreationDate { get; set; }
	}
}