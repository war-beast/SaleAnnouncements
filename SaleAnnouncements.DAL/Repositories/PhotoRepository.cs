using SaleAnnouncements.DAL.Data;
using SaleAnnouncements.DAL.Entities;
using SaleAnnouncements.DAL.Interfaces;
using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace SaleAnnouncements.DAL.Repositories
{
	public class PhotoRepository : RepositoryBase, IRepository<Photo>
	{
		#region constructor

		public PhotoRepository(ApplicationDbContext db) : base(db)
		{
		}

		#endregion

		public async Task<Photo> Get(Guid id)
		{
			return await _db.Photos
				.FirstAsync(x => x.Id.Equals(id));
		}

		public IQueryable<Photo> GetAll()
		{
			return _db.Photos.AsNoTracking();
		}

		public Guid Create(Photo item)
		{
			#region validation

			if (item == null)
				throw new ArgumentNullException(nameof(item));

			#endregion

			item.Id = Guid.NewGuid();
			_db.Photos.Add(item);

			return item.Id;
		}

		public void Update(Photo item)
		{
			#region validation

			if (item == null)
				throw new ArgumentNullException(nameof(item));

			#endregion

			var local = _db.Photos
				.First(x => x.Id.Equals(item.Id));

			_db.Entry(local).CurrentValues.SetValues(item);
		}

		public async Task Delete(Guid id)
		{
			var item = await _db.Photos.FirstAsync(x => x.Id == id);

			_db.Photos.Remove(item);
		}
	}
}
