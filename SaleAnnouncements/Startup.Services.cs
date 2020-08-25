using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using SaleAnnouncements.BLL.Interfaces;
using SaleAnnouncements.BLL.Services;
using SaleAnnouncements.DAL.Entities;
using SaleAnnouncements.DAL.Interfaces;
using SaleAnnouncements.DAL.Repositories;
using SaleAnnouncements.Util;

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

			services.AddTransient<ICategoryService, CategoryService>();
			services.AddTransient<IOfferStatusService, OfferStatusService>();
			services.AddTransient<IOfferService, OfferService>();

			#endregion

			#region presentation level

			services.AddTransient<IHomePageService, HomePageService>();

			#endregion
		}
	}
}
