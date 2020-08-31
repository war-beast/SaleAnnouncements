using System;
using System.Collections.Generic;
using System.Text;

namespace SaleAnnouncements.BLL.Model
{
	public class MessageThread
	{
		public Guid Id { get; set; }

		public string Name { get; set; } = string.Empty;

		public IReadOnlyCollection<SingleMessage> Messages { get; set; } = new List<SingleMessage>();
	}
}
