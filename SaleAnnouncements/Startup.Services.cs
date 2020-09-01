using Microsoft.Extensions.DependencyInjection;
using SaleAnnouncements.BLL.Interfaces;
using SaleAnnouncements.BLL.Services;
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

			services.AddTransient<ICategoryService, CategoryService>();
			services.AddTransient<IOfferStatusService, OfferStatusService>();
			services.AddTransient<IOfferService, OfferService>();
			services.AddTransient<ICustomerService, CustomerService>();
			services.AddTransient<IPhotoService, PhotoService>();
			services.AddTransient<IMessageService, MessageService>();
			services.AddTransient<ISearchService, SearchService>();

			#endregion

			#region presentation level

			services.AddTransient<IHomePageService, HomePageService>();
			services.AddTransient<ICategoryPageService, CategoryPageService>();

			#endregion
		}
	}
}
