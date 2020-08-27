using System;

namespace SaleAnnouncements.BLL.Dto
{
	public class PhotoDto : DtoBase
	{
		public byte[]? Image { get; set; }

		public Guid OfferId { get; set; }

		public OfferDto Offer { get; set; } = new OfferDto();
	}
}