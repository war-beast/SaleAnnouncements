using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using SaleAnnouncements.BLL.Dto;
using SaleAnnouncements.BLL.Interfaces;
using SaleAnnouncements.DAL.Entities;
using SaleAnnouncements.DAL.Interfaces;

namespace SaleAnnouncements.BLL.Services
{
	public class OfferStatusService : ServiceBase, IOfferStatusService
	{
		#region private members

		private readonly IMapper _mapper;

		#endregion

		#region constructor

		public OfferStatusService(IUnitOfWork unitOfWork, IMapper mapper) : base(unitOfWork)
		{
			_mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
		}

		#endregion

		public async Task<IReadOnlyCollection<OfferStatusDto>> GetAll()
		{
			var statuses = _unitOfWork.OfferStatuses
				.GetAll()
				.OrderBy(x => x.Price);
			return await Task.Run(() => _mapper.Map<List<OfferStatusDto>>(statuses));
		}

		public async Task<OfferStatusDto> Get(Guid id) =>
			_mapper.Map<OfferStatusDto>(await _unitOfWork.OfferStatuses.Get(id));

		public async Task AddCollection(IEnumerable<OfferStatusDto> items)
		{
			var coreStatuses = _mapper.Map<IEnumerable<OfferStatus>>(items);

			foreach (var status in coreStatuses)
			{
				_unitOfWork.OfferStatuses.Create(status);
			}

			await _unitOfWork.SaveAsync();
		}

		public void SetStatusForOffer(Guid offerId, IEnumerable<Guid> statusIds)
		{
			var ids = statusIds.ToList();
			if(ids.Count == 0)
				return;

			var offerStatuses = ids.Select(x => new OffersStatusesMap
			{
				OfferId = offerId,
				StatusId = x
			});

			foreach (var offerStatus in offerStatuses)
			{
				_unitOfWork.OffersStatusesMaps.Create(offerStatus);
			}
		}

		public async Task<IEnumerable<OfferStatusMapDto>> GetOfferStatusMaps(Guid offerId)
		{
			var offerStatuses = _unitOfWork.OffersStatusesMaps
				.GetAll()
				.Where(x => x.OfferId == offerId);

			return await Task.Run(() => 
				_mapper.Map<IEnumerable<OfferStatusMapDto>>(offerStatuses));
		}
	}
}
