using System;
using SaleAnnouncements.DAL.Entities;

namespace SaleAnnouncements.BLL.Dto
{
	public class OfferStatusMapDto : DtoBase
	{
		public Guid OfferId { get; set; }

		public Guid StatusId { get; set; }

		public Offer Offer { get; set; } = new Offer();

		public OfferStatus Status { get; set; } = new OfferStatus();

		public DateTime CreationDate { get; set; }
	}
}