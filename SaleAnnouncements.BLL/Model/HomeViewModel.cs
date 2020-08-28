using SaleAnnouncements.BLL.Dto;
using System.Collections.Generic;
using SaleAnnouncements.BLL.Interfaces;

namespace SaleAnnouncements.BLL.Model
{
	public class HomeViewModel
	{
		public IReadOnlyCollection<IListingItem> Categories { get; set; } = new List<CategoryDto>();
	}
}
