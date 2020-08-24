using SaleAnnouncements.DAL.Data;
using SaleAnnouncements.DAL.Entities;
using SaleAnnouncements.DAL.Interfaces;
using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace SaleAnnouncements.DAL.Repositories
{
	public class CategoryRepository : RepositoryBase, IRepository<Category>
	{
		#region constructor

		public CategoryRepository(ApplicationDbContext db) : base(db)
		{
		}

		#endregion

		public async Task<Category> Get(Guid id)
		{
			return await _db.Categories
				.Include(x => x.Offers)
				.FirstAsync(x => x.Id.Equals(id));
		}

		public IQueryable<Category> GetAll()
		{
			return _db.Categories
				.Include(x => x.Offers)
				.AsNoTracking();
		}

		public Guid Create(Category item)
		{
			#region validation

			if (item == null)
				throw new ArgumentNullException(nameof(item));

			#endregion

			item.Id = Guid.NewGuid();
			_db.Categories.Add(item);

			return item.Id;
		}

		public void Update(Category item)
		{
			#region validation

			if (item == null)
				throw new ArgumentNullException(nameof(item));

			#endregion

			var local = _db.Categories
				.First(x => x.Id.Equals(item.Id));

			_db.Entry(local).CurrentValues.SetValues(item);
		}

		public async Task Delete(Guid id)
		{
			var item = await _db.Categories.FirstAsync(x => x.Id == id);

			_db.Categories.Remove(item);
		}
	}
}
