using System;
using System.Collections.Generic;

namespace SaleAnnouncements.BLL.Dto
{
	public class OfferDto
	{
		public Guid Id { get; set; }

		public string Title { get; set; } = string.Empty;

		public string Description { get; set; } = string.Empty;

		public IEnumerable<PhotoDto> Photos { get; set; } = new List<PhotoDto>();

		public string CustomerId { get; set; } = string.Empty;

		public Guid CategoryId { get; set; }

		public IEnumerable<OfferStatusDto> Statuses { get; set; } = new List<OfferStatusDto>();

		public int Sort { get; set; } = 0;

		public DateTime CreationDate { get; set; }

		public DateTime UpdateDate { get; set; }

		public CategoryDto Category { get; set; } = new CategoryDto();
	}
}