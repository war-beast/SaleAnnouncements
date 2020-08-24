using System;
using System.Linq;
using System.Threading.Tasks;

namespace SaleAnnouncements.DAL.Interfaces
{
	public interface IRepository<T> where T: class
	{
		Task<T> Get(Guid id);

		IQueryable<T> GetAll();

		/// <summary>
		/// Создание новой записи
		/// </summary>
		/// <param name="item">Объект</param>
		/// <returns>ИД созданной записи</returns>
		Guid Create(T item);

		void Update(T item);

		Task Delete(Guid id);
	}
}
