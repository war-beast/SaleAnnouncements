using SaleAnnouncements.BLL.Model;
using System.Threading.Tasks;

namespace SaleAnnouncements.BLL.Interfaces
{
	public interface ISearchService
	{
		Task<SearchResultsViewModel> GetSearchResults(string phrase, string? userName = null);
	}
}
