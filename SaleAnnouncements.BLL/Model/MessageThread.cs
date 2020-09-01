using System;
using System.Collections.Generic;

namespace SaleAnnouncements.BLL.Model
{
	public class MessageThread
	{
		public Guid Id { get; set; }

		public string Name { get; set; } = string.Empty;

		public IReadOnlyCollection<SingleMessage> Messages { get; set; } = new List<SingleMessage>();

		/// <summary>
		/// ИД собеседника
		/// </summary>
		public Guid CompanionId { get; set; }
	}
}
