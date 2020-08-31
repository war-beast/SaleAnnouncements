using System;

namespace SaleAnnouncements.BLL.Dto
{
	public class MessageDto : DtoBase
	{
		public string Subject { get; set; } = string.Empty;

		public string Description { get; set; } = string.Empty;

		public Guid CustomerId { get; set; }

		/// <summary>
		/// ID собеседника
		/// </summary>
		public Guid CompanionId { get; set; }

		/// <summary>
		/// ID первого сообщения в диалоге
		/// </summary>
		public Guid? ParentId { get; set; }
	}
}
