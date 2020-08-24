using Microsoft.Extensions.DependencyInjection;
using SaleAnnouncements.BLL.Model.Settings;

namespace SaleAnnouncements
{
	public partial class Startup
	{
		public void ConfigureSettings(IServiceCollection services)
		{
			services.Configure<SiteSettings>(CustomConfiguration.GetSection("SiteSettings"));
		}
	}
}
