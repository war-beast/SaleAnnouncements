using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace SaleAnnouncements.Models
{
	public class OfferBindingModel
	{
		public Guid? Id { get; set; }

		[Required]
		public string Title { get; set; } = string.Empty;

		[Required]
		public string Description { get; set; } = string.Empty;

		public IEnumerable<IFormFile> Photos { get; set; }

		[Required]
		public Guid CategoryId { get; set; }

		public IEnumerable<Guid> SelectedStatusIds { get; set; } = new List<Guid>();

		[Required]
		public decimal Price { get; set; }

		[Required]
		public string PhoneNumber { get; set; } = string.Empty;
	}
}
