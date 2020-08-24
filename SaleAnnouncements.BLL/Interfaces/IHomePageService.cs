using System.Threading.Tasks;
using SaleAnnouncements.BLL.Model;

namespace SaleAnnouncements.BLL.Interfaces
{
	public interface IHomePageService
	{
		Task<HomeViewModel> GetPageModel();
	}
}
