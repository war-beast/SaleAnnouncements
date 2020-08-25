using Microsoft.EntityFrameworkCore;
using SaleAnnouncements.DAL.Data;
using SaleAnnouncements.DAL.Entities;
using SaleAnnouncements.DAL.Interfaces;
using System;
using System.Threading.Tasks;

namespace SaleAnnouncements.DAL.Repositories
{
	public class UnitOfWork : IUnitOfWork, IDisposable
	{
		#region private members

		private readonly ApplicationDbContext _db;
		private CategoryRepository _categoryRepository;
		private MessageRepository _messageRepository;
		private OfferRepository _offerRepository;
		private OfferStatusesRepository _offerStatusesRepository;
		private PhotoRepository _photoRepository;
		private OffersStatusesMapRepository _offersStatusesMapRepository;
		private CustomerRepository _customerRepository;

		private bool disposed = false;

		#endregion

		#region constructor

		public UnitOfWork(DbContextOptions<ApplicationDbContext> options)
		{
			if (options == null)
				throw new ArgumentNullException(nameof(options));

			_db = new ApplicationDbContext(options);
		}

		#endregion

		public IRepository<Category> Categories => _categoryRepository ??= new CategoryRepository(_db);

		public IRepository<Message> Messages => _messageRepository ??= new MessageRepository(_db);

		public IRepository<Offer> Offers => _offerRepository ??= new OfferRepository(_db);

		public IRepository<OfferStatus> OfferStatuses => _offerStatusesRepository ??= new OfferStatusesRepository(_db);

		public IRepository<Photo> Photos => _photoRepository ??= new PhotoRepository(_db);

		public IRepository<OffersStatusesMap> OffersStatusesMaps => _offersStatusesMapRepository ??= new OffersStatusesMapRepository(_db);

		public IRepository<Customer> Customers => _customerRepository ??= new CustomerRepository(_db);

		public async Task SaveAsync()
		{
			await _db.SaveChangesAsync();
		}

		#region IDisposable

		public virtual void Dispose(bool disposing)
		{
			if (disposed)
				return;

			if (disposing)
			{
				_db.Dispose();
			}
			this.disposed = true;
		}

		public void Dispose()
		{
			Dispose(true);
			GC.SuppressFinalize(this);
		}

		#endregion
	}
}
