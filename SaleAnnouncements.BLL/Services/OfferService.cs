using SaleAnnouncements.BLL.Dto;
using SaleAnnouncements.BLL.Interfaces;
using SaleAnnouncements.BLL.Model;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using SaleAnnouncements.DAL.Interfaces;

namespace SaleAnnouncements.BLL.Services
{
	public class OfferService : ServiceBase, IOfferService
	{
		#region private members

		private readonly IMapper _mapper;

		#endregion

		#region constructor

		public OfferService(IUnitOfWork unitOfWork, IMapper mapper) : base(unitOfWork)
		{
			_mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
		}

		#endregion

		public async Task<IReadOnlyCollection<OfferDto>> GetByCategory(Guid categoryId)
		{
			throw new NotImplementedException();
		}

		public async Task<Result> Create(OfferDto offer)
		{
			throw new NotImplementedException();
		}

		public async Task<OfferDto> Get(Guid id)
		{
			throw new NotImplementedException();
		}

		public async Task<Result> Update(OfferDto offer)
		{
			throw new NotImplementedException();
		}
	}
}
