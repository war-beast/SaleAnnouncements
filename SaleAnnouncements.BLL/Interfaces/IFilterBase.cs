using System;
using System.Collections.Generic;
using System.Text;

namespace SaleAnnouncements.BLL.Interfaces
{
	public interface IFilterBase
	{
		IEnumerable<Guid> Ids { get; set; }
	}
}
