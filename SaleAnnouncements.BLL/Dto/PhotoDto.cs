using System;

namespace SaleAnnouncements.BLL.Dto
{
	public class PhotoDto
	{
		public Guid Id { get; set; }

		public string FileName { get; set; }

		public Guid OfferId { get; set; }

		public string Ext { get; set; }

		public OfferDto Offer { get; set; }
	}
}