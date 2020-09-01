using SaleAnnouncements.BLL.Dto;
using System;
using System.Collections.Generic;

namespace SaleAnnouncements.BLL.Model
{
	public abstract class ListingViewModelBase
	{
		public string Title { get; set; } = string.Empty;

		public IReadOnlyCollection<OfferDto> Offers { get; set; } = new List<OfferDto>();

		public Guid? CurrentCustomerId { get; set; }
	}
}
