using Microsoft.EntityFrameworkCore;
using SaleAnnouncements.DAL.Data;
using SaleAnnouncements.DAL.Entities;
using SaleAnnouncements.DAL.Interfaces;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace SaleAnnouncements.DAL.Repositories
{
	public class MessageRepository : RepositoryBase, IRepository<Message>
	{
		#region constructor

		public MessageRepository(ApplicationDbContext db) : base(db)
		{
		}

		#endregion

		public async Task<Message> Get(Guid id)
		{
			return await _db.Messages
				.FirstAsync(x => x.Id.Equals(id));
		}

		public IQueryable<Message> GetAll()
		{
			return _db.Messages.AsNoTracking();
		}

		public Guid Create(Message item)
		{
			#region validation

			if (item == null)
				throw new ArgumentNullException(nameof(item));

			#endregion

			item.Id = Guid.NewGuid();
			_db.Messages.Add(item);

			return item.Id;
		}

		public void Update(Message item)
		{
			#region validation

			if (item == null)
				throw new ArgumentNullException(nameof(item));

			#endregion

			var local = _db.Messages
				.First(x => x.Id.Equals(item.Id));

			_db.Entry(local).CurrentValues.SetValues(item);
		}

		public async Task Delete(Guid id)
		{
			var item = await _db.Messages.FirstAsync(x => x.Id == id);

			_db.Messages.Remove(item);
		}
	}
}
