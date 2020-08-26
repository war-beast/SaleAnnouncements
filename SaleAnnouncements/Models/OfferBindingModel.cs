using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;
using SaleAnnouncements.BLL.Dto;

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

		public IEnumerable<OfferStatusMapDto> OffersStatuses { get; set; } = new List<OfferStatusMapDto>();

		[Required]
		public decimal Price { get; set; }

		[Required]
		public string PhoneNumber { get; set; } = string.Empty;
	}
}
