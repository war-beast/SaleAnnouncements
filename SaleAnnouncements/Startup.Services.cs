using Microsoft.Extensions.DependencyInjection;
using SaleAnnouncements.DAL.Interfaces;
using SaleAnnouncements.DAL.Repositories;

namespace SaleAnnouncements
{
	public partial class Startup
	{
		private void ConfigureCustomServices(IServiceCollection services)
		{
			#region data level

			services.AddScoped<IUnitOfWork, UnitOfWork>();

			#endregion

			#region businesslogic level

			

			#endregion

			#region presentation layer



			#endregion
		}
	}
}
