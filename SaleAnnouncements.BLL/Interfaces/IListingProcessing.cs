using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SaleAnnouncements.BLL.Interfaces
{
	public interface IListingProcessing<T> where T: class
	{
		Task<IReadOnlyCollection<T>> GetAll();

		Task<T> Get(Guid id);

		Task AddCollection(IEnumerable<T> items);
	}
}
