using System;

namespace SaleAnnouncements.BLL.Dto
{
	public class OfferStatusDto
	{
		public Guid Id { get; set; }

		public string Name { get; set; } = string.Empty;

		/// <summary>
		/// Величина, на которую может измениться позиция объявления
		/// </summary>
		public uint Amount { get; set; }

		/// <summary>
		/// Цена в рублях
		/// </summary>
		public decimal Price { get; set; }
	}
}