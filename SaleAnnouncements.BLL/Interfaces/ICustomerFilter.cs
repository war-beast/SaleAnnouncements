using System;
using System.Collections.Generic;

namespace SaleAnnouncements.BLL.Interfaces
{
	public interface ICustomerFilter : IFilterBase
	{
		string Email { get; set; }
	}
}