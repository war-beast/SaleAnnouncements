using SaleAnnouncements.BLL.Dto;
using SaleAnnouncements.BLL.Interfaces;
using SaleAnnouncements.BLL.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using SaleAnnouncements.DAL.Entities;
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
			var offers = _unitOfWork.Offers
				.GetAll()
				.Where(x => x.CategoryId == categoryId)
				.OrderByDescending(x =>x.Sort)
				.ThenByDescending(x => x.CreationDate);

			return _mapper.Map<List<OfferDto>>(offers.ToList());
		}

		public async Task<Result> Create(OfferDto offer)
		{
			var result = new Result
			{
				IsSuccess = true
			};

			try
			{
				result.EntityId = _unitOfWork.Offers.Create(_mapper.Map<Offer>(offer));
				await _unitOfWork.SaveAsync();
			}
			catch (Exception exc)
			{
				result = new Result
				{
					IsSuccess = false,
					Error = exc.Message,
					EntityId = Guid.Empty
				};
			}

			return result;
		}

		public async Task<OfferDto> Get(Guid id)
		{
			var offer = await _unitOfWork.Offers.Get(id);

			return _mapper.Map<OfferDto>(offer);
		}

		public async Task<Result> Update(OfferDto offer)
		{
			var result = new Result
			{
				IsSuccess = true,
				EntityId = offer.Id ?? Guid.Empty
			};

			try
			{
				_unitOfWork.Offers.Update(_mapper.Map<Offer>(offer));
				await _unitOfWork.SaveAsync();
			}
			catch (Exception exc)
			{
				result = new Result
				{
					IsSuccess = false,
					Error = exc.Message,
					EntityId = Guid.Empty
				};
			}

			return result;
		}
	}
}
