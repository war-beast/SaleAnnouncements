using System;

namespace SaleAnnouncements.DAL.Entities
{
	public class Photo : EntityBase
	{
		public byte[] Image { get; set; }

		public Guid OfferId { get; set; }
	}
}
