using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using SaleAnnouncements.BLL.Dto;

namespace SaleAnnouncements.BLL.Model
{
	public class OfferBindingModel
	{
		[Required]
		public string Title { get; set; } = string.Empty;

		[Required]
		public string Description { get; set; } = string.Empty;

		public IEnumerable<PhotoDto> Photos { get; set; } = new List<PhotoDto>();

		[Required]
		public string CustomerId { get; set; } = string.Empty;

		[Required]
		public Guid CategoryId { get; set; }

		public IEnumerable<OfferStatusMapDto> OffersStatuses { get; set; } = new List<OfferStatusMapDto>();

		public int Sort { get; set; } = 0;

		public DateTime CreationDate { get; set; }

		public DateTime UpdateDate { get; set; }

		public CategoryDto Category { get; set; } = new CategoryDto();
	}
}
