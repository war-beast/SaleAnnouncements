using System;

namespace SaleAnnouncements.BLL.Dto
{
	public class PhotoDto : DtoBase
	{
		public string FileName { get; set; } = string.Empty;

		public Guid OfferId { get; set; }

		public OfferDto Offer { get; set; } = new OfferDto();
	}
}