using System;
using System.Collections.Generic;
using System.Text;
using SaleAnnouncements.DAL.Data;

namespace SaleAnnouncements.DAL.Repositories
{
	public abstract class RepositoryBase
	{
		protected readonly ApplicationDbContext _db;

		protected RepositoryBase(ApplicationDbContext db)
		{
			_db = db ?? throw new ArgumentNullException(nameof(db));
		}
	}
}
