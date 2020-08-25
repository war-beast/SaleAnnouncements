using System;
using System.Collections.Generic;
using System.Text;
using SaleAnnouncements.BLL.Interfaces;

namespace SaleAnnouncements.BLL.Model.Filters
{
	public class CustomerFilterBuilder
	{
		#region private members

		private readonly ICustomerFilter _customerFilter;

		#endregion

		#region constructor

		public CustomerFilterBuilder()
		{
			_customerFilter = new CustomerFilter();
		}

		#endregion

		public CustomerFilterBuilder SetIds(IEnumerable<Guid> ids)
		{
			_customerFilter.Ids = ids;
			return this;
		}

		public CustomerFilterBuilder SetEmail(string email)
		{
			_customerFilter.Email = email;
			return this;
		}

		public ICustomerFilter Build()
		{
			return _customerFilter;
		}
	}
}
