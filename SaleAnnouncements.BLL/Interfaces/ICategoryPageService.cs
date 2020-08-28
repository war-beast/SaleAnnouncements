using SaleAnnouncements.BLL.Model;
using System;
using System.Threading.Tasks;

namespace SaleAnnouncements.BLL.Interfaces
{
	public interface ICategoryPageService
	{
		Task<CategoryViewModel> GetPage(Guid id);
	}
}
