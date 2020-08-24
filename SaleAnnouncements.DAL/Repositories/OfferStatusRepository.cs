using SaleAnnouncements.DAL.Data;
using SaleAnnouncements.DAL.Entities;
using SaleAnnouncements.DAL.Interfaces;
using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace SaleAnnouncements.DAL.Repositories
{
	public class OfferStatusesRepository : RepositoryBase, IRepository<OfferStatus>
	{
		#region constructor

		public OfferStatusesRepository(ApplicationDbContext db) : base(db)
		{
		}

		#endregion

		public async Task<OfferStatus> Get(Guid id)
		{
			return await _db.OfferStatuses
				.FirstAsync(x => x.Id.Equals(id));
		}

		public IQueryable<OfferStatus> GetAll()
		{
			return _db.OfferStatuses.AsNoTracking();
		}

		public void Create(OfferStatus item)
		{
			#region validation

			if (item == null)
				throw new ArgumentNullException(nameof(item));

			#endregion

			item.Id = Guid.NewGuid();
			_db.OfferStatuses.Add(item);
		}

		public void Update(OfferStatus item)
		{
			#region validation

			if (item == null)
				throw new ArgumentNullException(nameof(item));

			#endregion

			var local = _db.OfferStatuses
				.First(x => x.Id.Equals(item.Id));

			_db.Entry(local).CurrentValues.SetValues(item);
		}

		public async Task Delete(Guid id)
		{
			var item = await _db.OfferStatuses.FirstAsync(x => x.Id == id);

			_db.OfferStatuses.Remove(item);
		}
	}
}
