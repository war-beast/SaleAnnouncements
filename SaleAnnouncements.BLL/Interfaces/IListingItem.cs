using System;
using System.Collections.Generic;
using System.Text;

namespace SaleAnnouncements.BLL.Interfaces
{
	public interface IListingItem
	{
		Guid? Id { get; }

		string Caption { get; }

		int DependenciesCount { get; }
	}
}
