﻿using System.IO;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using SaleAnnouncements.DAL.Entities;

namespace SaleAnnouncements.DAL.Data
{
	public sealed class ApplicationDbContext : DbContext
	{
		public DbSet<Offer> Offers { get; set; }
		public DbSet<Message> Messages { get; set; }
		public DbSet<Photo> Photos { get; set; }
		public DbSet<Category> Categories { get; set; }
		public DbSet<OfferStatus> OfferStatuses { get; set; }
		public DbSet<OffersStatusesMap> OffersStatusesMaps { get; set; }

		public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
			: base(options)
		{
			Database.EnsureCreated();
		}

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			base.OnModelCreating(modelBuilder);

			modelBuilder.Entity<Customer>()
				.HasMany(x => x.Messages)
				.WithOne(x => x.Customer)
				.HasForeignKey(x => x.CustomerId)
				.OnDelete(DeleteBehavior.Cascade);

			modelBuilder.Entity<Customer>()
				.HasMany(x => x.SalesOffers)
				.WithOne(x => x.Customer)
				.HasForeignKey(x => x.CustomerId)
				.OnDelete(DeleteBehavior.Cascade);

			modelBuilder.Entity<Offer>()
				.HasMany(x => x.Photos)
				.WithOne(x => x.Offer)
				.HasForeignKey(x => x.OfferId)
				.OnDelete(DeleteBehavior.Cascade);

			modelBuilder.Entity<Offer>()
				.HasOne(x => x.Category)
				.WithMany(x => x.Offers)
				.HasForeignKey(x => x.CategoryId);

			modelBuilder.Entity<OffersStatusesMap>()
				.HasOne(x => x.Offer)
				.WithMany(x => x.OffersStatuses)
				.HasForeignKey(x => x.OfferId);

			modelBuilder.Entity<OffersStatusesMap>()
				.HasOne(x => x.Status)
				.WithMany(x => x.OffersStatuses)
				.HasForeignKey(x => x.StatusId);
		}
	}

	public class ApplicationContextFactory : IDesignTimeDbContextFactory<ApplicationDbContext>
	{
		ApplicationDbContext IDesignTimeDbContextFactory<ApplicationDbContext>.CreateDbContext(string[] args)
		{
			var config = new ConfigurationBuilder()
				.SetBasePath(Path.Combine(Directory.GetCurrentDirectory()))
				.AddJsonFile("appsettings.json", optional: true).Build();

			var optionsBuilder = new DbContextOptionsBuilder<ApplicationDbContext>();
			optionsBuilder.UseSqlServer((string) config.GetConnectionString("DefaultConnection"));

			return new ApplicationDbContext(optionsBuilder.Options);
		}
	}
}
