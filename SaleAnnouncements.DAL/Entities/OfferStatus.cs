using System.ComponentModel.DataAnnotations.Schema;

namespace SaleAnnouncements.DAL.Entities
{
	public class OfferStatus : EntityBase
	{
		public string Name { get; set; }

		/// <summary>
		/// Величина, на которую может измениться позиция объявления
		/// </summary>
		public uint Amount { get; set; }

		/// <summary>
		/// Цена в рублях
		/// </summary>
		[Column(TypeName = "decimal(18, 2)")]
		public decimal Price { get; set; }
	}
}
