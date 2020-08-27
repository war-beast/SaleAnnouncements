using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using SaleAnnouncements.BLL.Dto;
using SaleAnnouncements.BLL.Model;

namespace SaleAnnouncements.BLL.Interfaces
{
	public interface IPhotoService
	{
		Task<PhotoDto> Get(Guid id);

		Task<IReadOnlyCollection<PhotoDto>> GetForOffer(Guid offerId);

		Task<Result> Save(IList<PhotoDto> photos, Guid offerId);

		Task<Result> Delete(Guid id);
	}
}
