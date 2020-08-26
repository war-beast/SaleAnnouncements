using AutoMapper;
using Microsoft.Extensions.DependencyInjection;
using SaleAnnouncements.BLL.Mapping;
using SaleAnnouncements.Mapping;

namespace SaleAnnouncements
{
	public partial class Startup
	{
		private void ConfigureAutomapper(IServiceCollection services)
		{
			var config = new MapperConfiguration(cfg => {
				cfg.AddProfile(new MappingProfiles());
				cfg.AddProfile(new PresentationMappingProfiles());
			});

			IMapper mapper = config.CreateMapper();
			services.AddSingleton(mapper);
		}
	}
}
