using System;

namespace SaleAnnouncements.DAL.Entities
{
	public class OffersStatusesMap : EntityBase
	{
		public Guid OfferId { get; set; }

		public Guid StatusId { get; set; }

		public DateTime CreationDate { get; set; }

		#region navigation properties

		public Offer Offer { get; set; }

		public OfferStatus Status { get; set; }

		#endregion
	}
}
