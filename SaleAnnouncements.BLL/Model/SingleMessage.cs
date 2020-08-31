namespace SaleAnnouncements.BLL.Model
{
	public class SingleMessage
	{
		public string Name { get; set; } = string.Empty;

		public string Date { get; set; } = string.Empty;

		public string Message { get; set; } = string.Empty;

		public bool IsMyMessage { get; set; }
	}
}