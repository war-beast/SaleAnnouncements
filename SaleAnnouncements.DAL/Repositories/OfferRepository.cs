using SaleAnnouncements.DAL.Data;
using SaleAnnouncements.DAL.Entities;
using SaleAnnouncements.DAL.Interfaces;
using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace SaleAnnouncements.DAL.Repositories
{
	public class OfferRepository : RepositoryBase, IRepository<Offer>
	{
		#region constructor

		public OfferRepository(ApplicationDbContext db) : base(db)
		{
		}

		#endregion

		public async Task<Offer> Get(Guid id)
		{
			return await _db.Offers
				.FirstAsync(x => x.Id.Equals(id));
		}

		public IQueryable<Offer> GetAll()
		{
			return _db.Offers.AsNoTracking();
		}

		public void Create(Offer item)
		{
			#region validation

			if (item == null)
				throw new ArgumentNullException(nameof(item));

			#endregion

			item.Id = Guid.NewGuid();
			item.CreationDate = DateTime.Now;
			item.UpdateDate = DateTime.Now;

			_db.Offers.Add(item);
		}

		public void Update(Offer item)
		{
			#region validation

			if (item == null)
				throw new ArgumentNullException(nameof(item));

			#endregion

			item.UpdateDate = DateTime.Now;

			var local = _db.Offers
				.First(x => x.Id.Equals(item.Id));

			_db.Entry(local).CurrentValues.SetValues(item);
		}

		public async Task Delete(Guid id)
		{
			var item = await _db.Offers.FirstAsync(x => x.Id == id);

			_db.Offers.Remove(item);
		}
	}
}
