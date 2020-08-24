using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using SaleAnnouncements.BLL.Interfaces;
using SaleAnnouncements.Util;
using System;
using System.Threading.Tasks;

namespace SaleAnnouncements
{
	public class Program
	{
		public static async Task Main(string[] args)
		{
			var host = CreateHostBuilder(args).Build();

			using (var scope = host.Services.CreateScope())
			{
				var services = scope.ServiceProvider;
				try
				{
					var categoryService = services.GetRequiredService<ICategoryService>();
					var offerStatusService = services.GetRequiredService<IOfferStatusService>();

					//Проинициализируем категории и платные статусы в БД
					await DataInit.InitData(categoryService, offerStatusService);
				}
				catch (Exception exc)
				{
					var logger = services.GetRequiredService<ILogger<Program>>();
					logger.LogError(exc, "Произошла ошибка при заполнении БД начальными данными");
				}
			}

			await host.RunAsync();
		}

		public static IHostBuilder CreateHostBuilder(string[] args) =>
			Host.CreateDefaultBuilder(args)
				.ConfigureWebHostDefaults(webBuilder =>
				{
					webBuilder.UseStartup<Startup>();
				});
	}
}
