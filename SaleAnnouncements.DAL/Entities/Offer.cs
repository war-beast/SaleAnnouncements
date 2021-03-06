﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace SaleAnnouncements.DAL.Entities
{
	public class Offer : EntityBase
	{
		public string Title { get; set; }

		public string Description { get; set; }

		public IEnumerable<Photo> Photos { get; set; }

		public Guid CustomerId { get; set; }

		public Guid CategoryId { get; set; }

		public IEnumerable<OffersStatusesMap> OffersStatuses { get; set; }

		/// <summary>
		/// Положение объявления в списке. Вычисляется в зависимости от купленных статусов и истечения их срока.
		/// </summary>
		public int Sort { get; set; } = 0;

		public DateTime CreationDate { get; set; }

		public DateTime UpdateDate { get; set; }

		[Column(TypeName = "decimal(18, 2)")]
		public decimal Price { get; set; }

		/// <summary>
		/// Разрешаем пользователю указывать разные номера телефонов в разных объявлениях
		/// </summary>
		public string PhoneNumber { get; set; }
	}
}
