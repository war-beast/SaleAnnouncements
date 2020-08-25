using SaleAnnouncements.BLL.Dto;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SaleAnnouncements.BLL.Interfaces
{
	public interface ICustomerService
	{
		Task<CustomerDto> Get(Guid id);

		Task<IReadOnlyCollection<CustomerDto>> GetFiltered(ICustomerFilter filter);

		Task<Guid> Create(string userId);
	}
}
