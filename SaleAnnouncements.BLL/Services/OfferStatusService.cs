using System;
using System.Collections.Generic;
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
			var statuses = _unitOfWork.OfferStatuses.GetAll();
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
	}
}
