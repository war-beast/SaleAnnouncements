using System.Collections.Generic;
using Microsoft.AspNetCore.Identity;

namespace SaleAnnouncements.DAL.Entities
{
	public class Customer : EntityBase
	{
		public string UserId { get; set; }

		public IEnumerable<Offer> SalesOffers { get; set; }

		public IEnumerable<Message> Messages { get; set; }
	}
}
