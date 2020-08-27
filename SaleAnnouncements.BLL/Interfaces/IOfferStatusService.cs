using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using SaleAnnouncements.BLL.Dto;

namespace SaleAnnouncements.BLL.Interfaces
{
	public interface IOfferStatusService : IListingProcessing<OfferStatusDto>
	{
		void SetStatusForOffer(Guid offerId, IEnumerable<Guid> statusIds);

		Task<IEnumerable<OfferStatusMapDto>> GetOfferStatusMaps(Guid offerId);
	}
}
