using System;
using System.Collections.Generic;
using System.Linq;
using SaleAnnouncements.BLL.Interfaces;

namespace SaleAnnouncements.BLL.Dto
{
	public class CategoryDto : DtoBase, IListingItem
	{
		public string Name { get; set; } = string.Empty;

		public IEnumerable<OfferDto> Offers { get; set; } = new List<OfferDto>();

		#region IListingItem

		Guid? IListingItem.Id => Id;

		public string Caption => Name;

		public int DependenciesCount => Offers.Count();

		#endregion
	}
}
