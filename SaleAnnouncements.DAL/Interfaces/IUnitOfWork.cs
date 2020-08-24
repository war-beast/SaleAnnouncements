using SaleAnnouncements.DAL.Entities;
using System.Threading.Tasks;

namespace SaleAnnouncements.DAL.Interfaces
{
	public interface IUnitOfWork
	{
		IRepository<Category> Categories { get; }

		IRepository<Message> Messages { get; }

		IRepository<Offer> Offers { get; }

		IRepository<OfferStatus> OfferStatuses { get; }

		IRepository<Photo> Photos { get; }

		IRepository<OffersStatusesMap> OffersStatusesMaps { get; }

		Task SaveAsync();
	}
}
