using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace SaleAnnouncements.Models
{
	public class NewStatusBindingModel
	{
		[Required]
		public Guid Id { get; set; }

		[Required]
		public IEnumerable<Guid> SelectedStatusIds { get; set; } = new List<Guid>();
	}
}
