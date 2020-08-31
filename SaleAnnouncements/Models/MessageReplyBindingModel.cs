using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SaleAnnouncements.Models
{
	public class MessageReplyBindingModel
	{
		[Required]
		public Guid ParentMessageId { get; set; }

		[Required]
		public Guid CurrentCustomerId { get; set; }

		[Required]
		public Guid CompanionId { get; set; }

		[Required]
		public string Message { get; set; }
	}
}
