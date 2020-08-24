using SaleAnnouncements.DAL.Interfaces;
using System;

namespace SaleAnnouncements.BLL.Services
{
	public abstract class ServiceBase
	{
		#region private members

		protected readonly IUnitOfWork _unitOfWork;

		#endregion

		protected ServiceBase(IUnitOfWork unitOfWork)
		{
			_unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
		}
	}
}
