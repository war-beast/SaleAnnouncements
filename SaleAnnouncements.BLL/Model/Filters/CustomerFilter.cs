using SaleAnnouncements.BLL.Interfaces;
using System;
using System.Collections.Generic;

namespace SaleAnnouncements.BLL.Model.Filters
{
	public class CustomerFilter : ICustomerFilter
	{
		public CustomerFilter()
		{
			Ids = new List<Guid>();
			Email = string.Empty;
		}

		public static CustomerFilterBuilder CreateBuilder()
		{
			return new CustomerFilterBuilder();
		}

		public IEnumerable<Guid> Ids { get; set; }

		public string Email { get; set; }
	}
}
