using Microsoft.EntityFrameworkCore;
using SaleAnnouncements.DAL.Data;
using SaleAnnouncements.DAL.Entities;
using SaleAnnouncements.DAL.Interfaces;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace SaleAnnouncements.DAL.Repositories
{
	public class OffersStatusesMapRepository : RepositoryBase, IRepository<OffersStatusesMap>
	{
		#region constructor
		
		public OffersStatusesMapRepository(ApplicationDbContext db) : base(db)
		{
		}

		#endregion

		public async Task<OffersStatusesMap> Get(Guid id)
		{
			return await _db.OffersStatusesMaps
				.Include(x => x.Offer)
				.Include(x => x.Status)
				.FirstAsync(x => x.Id.Equals(id));
		}

		public IQueryable<OffersStatusesMap> GetAll()
		{
			return _db.OffersStatusesMaps
				.Include(x => x.Offer)
				.Include(x => x.Status)
				.AsNoTracking();
		}

		public Guid Create(OffersStatusesMap item)
		{
			#region validation

			if (item == null)
				throw new ArgumentNullException(nameof(item));

			#endregion

			item.Id = Guid.NewGuid();
			item.CreationDate = DateTime.Now;
			_db.OffersStatusesMaps.Add(item);

			return item.Id;
		}

		public void Update(OffersStatusesMap item)
		{
			#region validation

			if (item == null)
				throw new ArgumentNullException(nameof(item));

			#endregion

			var local = _db.OffersStatusesMaps
				.First(x => x.Id.Equals(item.Id));

			_db.Entry(local).CurrentValues.SetValues(item);
		}

		public async Task Delete(Guid id)
		{
			var item = await _db.OffersStatusesMaps.FirstAsync(x => x.Id == id);

			_db.OffersStatusesMaps.Remove(item);
		}
	}
}
