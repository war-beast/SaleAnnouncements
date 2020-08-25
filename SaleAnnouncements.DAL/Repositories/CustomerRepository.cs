using SaleAnnouncements.DAL.Data;
using SaleAnnouncements.DAL.Entities;
using SaleAnnouncements.DAL.Interfaces;
using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace SaleAnnouncements.DAL.Repositories
{
	public class CustomerRepository : RepositoryBase, IRepository<Customer>
	{
		#region constructor

		public CustomerRepository(ApplicationDbContext db) : base(db)
		{
		}

		#endregion

		public async Task<Customer> Get(Guid id)
		{
			return await _db.Customers
				.Include(x => x.Messages)
				.Include(x => x.SalesOffers)
				.FirstAsync(x => x.Id.Equals(id));
		}

		public IQueryable<Customer> GetAll()
		{
			return _db.Customers
				.Include(x => x.Messages)
				.Include(x => x.SalesOffers)
				.AsNoTracking();
		}

		public Guid Create(Customer item)
		{
			#region validation

			if (item == null)
				throw new ArgumentNullException(nameof(item));

			#endregion

			item.Id = Guid.NewGuid();
			_db.Customers.Add(item);

			return item.Id;
		}

		public void Update(Customer item)
		{
			#region validation

			if (item == null)
				throw new ArgumentNullException(nameof(item));

			#endregion

			var local = _db.Customers
				.First(x => x.Id.Equals(item.Id));

			_db.Entry(local).CurrentValues.SetValues(item);
			_db.Entry(local.Messages).CurrentValues.SetValues(item.Messages);
			_db.Entry(local.SalesOffers).CurrentValues.SetValues(item.SalesOffers);
		}

		public async Task Delete(Guid id)
		{
			var item = await _db.Customers.FirstAsync(x => x.Id == id);

			_db.Customers.Remove(item);
		}
	}
}
