using System;

namespace SaleAnnouncements.DAL.Entities
{
	public class Message : EntityBase
	{
		public string Subject { get; set; }

		public string Description { get; set; }

		public Guid CustomerId { get; set; }

		/// <summary>
		/// ID собеседника
		/// </summary>
		public Guid CompanionId { get; set; }

		/// <summary>
		/// ID первого сообщения в диалоге
		/// </summary>
		public Guid? ParentId { get; set; }

		public DateTime CreationDate { get; set; }
	}
}
