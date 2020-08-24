using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using SaleAnnouncements.DAL.Entities;

namespace SaleAnnouncements.DAL.Data
{
	public sealed class ApplicationDbContext : IdentityDbContext<Customer>
	{
		public DbSet<Offer> Offers { get; set; }
		public DbSet<Message> Messages { get; set; }
		public DbSet<Photo> Photos { get; set; }
		public DbSet<Category> Categories { get; set; }
		public DbSet<OfferStatus> OfferStatuses { get; set; }

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
		}
	}
}
