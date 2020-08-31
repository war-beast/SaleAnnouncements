using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using SaleAnnouncements.BLL.Dto;
using SaleAnnouncements.BLL.Model;

namespace SaleAnnouncements.BLL.Interfaces
{
	public interface IMessageService
	{
		Task<Result> SaveMessage(MessageDto message);

		Task<IReadOnlyCollection<MessageTitle>> GetUserMessageTitles(Guid customerId);
	}
}
