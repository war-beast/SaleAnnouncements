using System;

namespace SaleAnnouncements.BLL.Dto
{
	public class MessageDto : DtoBase
	{
		public string Subject { get; set; } = string.Empty;

		public string Description { get; set; } = string.Empty;

		public string CustomerId { get; set; } = string.Empty;

		/// <summary>
		/// ID собеседника
		/// </summary>
		public string CompanionId { get; set; } = string.Empty;

		/// <summary>
		/// ID первого сообщения в диалоге
		/// </summary>
		public Guid? ParentId { get; set; }
	}
}
