using System;
using System.Collections.Generic;

namespace SaleAnnouncements.BLL.Dto
{
	public class OfferDto : DtoBase
	{
		public string Title { get; set; } = string.Empty;

		public string Description { get; set; } = string.Empty;

		public IEnumerable<PhotoDto> Photos { get; set; } = new List<PhotoDto>();

		public Guid CustomerId { get; set; }

		public Guid CategoryId { get; set; }

		public IEnumerable<OfferStatusMapDto> OffersStatuses { get; set; } = new List<OfferStatusMapDto>(); 
		
		public IEnumerable<Guid> SelectedStatusIds { get; set; } = new List<Guid>();

		public int Sort { get; set; } = 0;

		public DateTime CreationDate { get; set; }

		public DateTime UpdateDate { get; set; }

		public CategoryDto Category { get; set; } = new CategoryDto();

		public decimal Price { get; set; }

		public string PhoneNumber { get; set; } = string.Empty;
	}
}