using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;

namespace SaleAnnouncements.DAL.Entities
{
	public class Customer : IdentityUser
	{
		public IEnumerable<Offer> SalesOffers { get; set; }

		public IEnumerable<Message> Messages { get; set; }
	}
}
