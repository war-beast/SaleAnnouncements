using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using SaleAnnouncements.BLL.Dto;

namespace SaleAnnouncements.BLL.Interfaces
{
	public interface ICategoryService
	{
		Task<IReadOnlyCollection<CategoryDto>> GetCategories();

		Task<CategoryDto> GetCategory(Guid id);

		void AddCategories(IEnumerable<CategoryDto> categories);
	}
}
