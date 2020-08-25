using SaleAnnouncements.BLL.Dto;
using SaleAnnouncements.BLL.Model;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SaleAnnouncements.BLL.Interfaces
{
	public interface IOfferService
	{
		Task<IReadOnlyCollection<OfferDto>> GetByCategory(Guid categoryId);

		Task<IReadOnlyCollection<OfferDto>> GetForUser(Guid id);

		Task<Result> Create(OfferDto offer);

		Task<OfferDto> Get(Guid id);

		Task<Result> Update(OfferDto offer);
	}
}