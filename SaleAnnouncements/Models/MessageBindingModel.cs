using System;
using System.ComponentModel.DataAnnotations;

namespace SaleAnnouncements.Models
{
	public class MessageBindingModel
	{
		public Guid? HostMessageId { get; set; }

		[Required]
		public Guid CurrentCustomerId { get; set; }

		[Required]
		public Guid OfferOwnerId { get; set; }

		[Required]
		public string Message { get; set; }

		[Required]
		public Guid OfferId { get; set; }
	}
}
