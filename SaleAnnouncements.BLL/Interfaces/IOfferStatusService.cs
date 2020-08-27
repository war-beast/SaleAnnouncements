using System;
using System.Collections.Generic;
using SaleAnnouncements.BLL.Dto;

namespace SaleAnnouncements.BLL.Interfaces
{
	public interface IOfferStatusService : IListingProcessing<OfferStatusDto>
	{
		void SetStatusForOffer(Guid offerId, IEnumerable<Guid> statusIds);
	}
}
