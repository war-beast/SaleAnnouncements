using System;

namespace SaleAnnouncements.DAL.Entities
{
	public class Photo : EntityBase
	{
		public string FileName { get; set; }

		public Guid OfferId { get; set; }

		#region navigation

		public Offer Offer { get; set; }

		#endregion
	}
}
