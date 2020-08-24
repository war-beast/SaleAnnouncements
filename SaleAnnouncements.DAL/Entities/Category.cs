using System.Collections.Generic;

namespace SaleAnnouncements.DAL.Entities
{
	public class Category : EntityBase
	{
		public string Name { get; set; }

		public IEnumerable<Offer> Offers { get; set; }
	}
}
