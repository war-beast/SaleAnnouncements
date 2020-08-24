using System;

namespace SaleAnnouncements.DAL.Entities
{
	public class Message : EntityBase
	{
		public string Subject { get; set; }

		public string Description { get; set; }

		public string CustomerId { get; set; }

		/// <summary>
		/// ID собеседника
		/// </summary>
		public string CompanionId { get; set; }

		/// <summary>
		/// ID первого сообщения в диалоге
		/// </summary>
		public Guid? ParentId { get; set; }

		#region navigation

		public Customer Customer { get; set; }

		#endregion
	}
}
