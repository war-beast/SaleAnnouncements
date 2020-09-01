using AutoMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using SaleAnnouncements.DAL.Data;
using SaleAnnouncements.Util;
using System.IO;
using System.Net;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using SaleAnnouncements.Extensions;

namespace SaleAnnouncements
{
	public partial class Startup
	{
		public Startup(IConfiguration configuration)
		{
			Configuration = configuration;

			var builder = new ConfigurationBuilder()
				.SetBasePath(Directory.GetCurrentDirectory())
				.AddJsonFile("custom.json", true, true);

			CustomConfiguration = builder.Build();
		}

		public IConfiguration Configuration { get; }

		public IConfiguration CustomConfiguration { get; }

		public void ConfigureServices(IServiceCollection services)
		{
			var connectionString = Configuration.GetConnectionString("DefaultConnection");

			services.AddDbContext<IdentityContext>(options =>
				options.UseSqlServer(connectionString));

			services.AddDbContext<ApplicationDbContext>(options =>
				options.UseSqlServer(connectionString));

			services.AddDefaultIdentity<IdentityUser>(options =>
			{
				options.SignIn.RequireConfirmedAccount = false;
				options.SignIn.RequireConfirmedEmail = false;
				options.SignIn.RequireConfirmedPhoneNumber = false;
				options.Password.RequireNonAlphanumeric = false;
				options.Password.RequireUppercase = false;
				options.Password.RequireDigit = false;
				options.Password.RequireLowercase = false;
				options.Password.RequiredLength = 4;
				options.User.RequireUniqueEmail = true;
				options.User.AllowedUserNameCharacters = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789-._@+";
			})
				.AddEntityFrameworkStores<IdentityContext>()
				.AddErrorDescriber<CustomIdentityErrorDescriber>();

			services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
				.AddJwtBearer(options =>
				{
					options.RequireHttpsMetadata = true;
					options.TokenValidationParameters = new TokenValidationParameters
					{
						// укзывает, будет ли валидироваться издатель при валидации токена
						ValidateIssuer = true,
						// строка, представляющая издателя
						ValidIssuer = AuthOptions.ISSUER,

						// будет ли валидироваться потребитель токена
						ValidateAudience = true,
						// установка потребителя токена
						ValidAudience = AuthOptions.AUDIENCE,
						// будет ли валидироваться время существования
						ValidateLifetime = true,

						// установка ключа безопасности
						IssuerSigningKey = AuthOptions.GetSymmetricSecurityKey(),
						// валидация ключа безопасности
						ValidateIssuerSigningKey = true
					};
					options.SaveToken = true;
				});

			services.AddControllersWithViews();
			services.AddRazorPages();

			services.AddAutoMapper(typeof(Startup));

			ConfigureAutomapper(services);
			ConfigureCustomServices(services);

			var defaultCacheProfile = new CacheProfile();
			Configuration.GetSection("CacheProfiles:Default").Bind(defaultCacheProfile);

			services.AddMvc(option =>
			{
				option.CacheProfiles.Add("Default", defaultCacheProfile);
			});
		}

		public void Configure(IApplicationBuilder app, IWebHostEnvironment env, ILoggerFactory loggerFactory)
		{
			if (env.IsDevelopment())
			{
				app.UseDeveloperExceptionPage();
				app.UseDatabaseErrorPage();
			}
			else
			{
				app.UseExceptionHandler("/Home/Error");
				// The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
				app.UseHsts();
			}


			loggerFactory.AddFile(Path.Combine(Directory.GetCurrentDirectory(), "application-log.txt"));

			app.UseJwtCookie();
			app.UseHttpsRedirection();
			app.UseStaticFiles(new StaticFileOptions()
			{
				OnPrepareResponse = ctx =>
				{
					ctx.Context.Response.Headers.Add("Cache-Control", "public,max-age=1200");
				}
			});

			app.UseResponseCaching();

			app.UseRouting();

			app.UseAuthentication();
			app.UseAuthorization();

			app.UseStatusCodePages(async context =>
			{
				var response = context.HttpContext.Response;

				var isNotApiRequest = !context.HttpContext.Request.Path.StartsWithSegments("/api");
				var forbidden = response.StatusCode == (int)HttpStatusCode.Forbidden;
				var unauthorized = response.StatusCode == (int)HttpStatusCode.Unauthorized;

				if (isNotApiRequest && (unauthorized || forbidden))
				{
					response.Redirect("/Identity/Account/SignIn");
				}
			});

			app.UseEndpoints(endpoints =>
			{
				endpoints.MapControllerRoute(
					name: "default",
					pattern: "{controller=Home}/{action=Index}/{id?}");
				endpoints.MapRazorPages();
			});
		}
	}
}
