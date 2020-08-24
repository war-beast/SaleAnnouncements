using System;

namespace SaleAnnouncements.BLL.Model
{
	public class Result
	{
		public bool IsSuccess { get; set; }

		public string Error { get; set; } = string.Empty;

		public Guid EntityId { get; set; }
	}
}
