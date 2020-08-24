using System;
using System.Collections.Generic;

namespace SaleAnnouncements.DAL.Entities
{
	public class Offer : EntityBase
	{
		public string Title { get; set; }

		public string Description { get; set; }

		public IEnumerable<Photo> Photos { get; set; }

		public string CustomerId { get; set; }

		public Guid CategoryId { get; set; }

		public IEnumerable<OfferStatus> Statuses { get; set; }

		/// <summary>
		/// Положение объявления в списке. Вычисляется в зависимости от купленных статусов и истечения их срока.
		/// </summary>
		public int Sort { get; set; } = 0;

		public DateTime CreationDate { get; set; }

		public DateTime UpdateDate { get; set; }

		#region navigation

		public Customer Customer { get; set; }

		public Category Category { get; set; }

		#endregion
	}
}
