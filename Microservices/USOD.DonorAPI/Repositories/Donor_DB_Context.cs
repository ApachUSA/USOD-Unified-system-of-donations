using Donor_Library.Entity;
using Microsoft.EntityFrameworkCore;
using System;

namespace USOD.DonorAPI.Repositories
{
    public class Donor_DB_Context: DbContext
	{
		public Donor_DB_Context(DbContextOptions<Donor_DB_Context> options) : base(options)
		{
			Database.EnsureCreated();
		}

		public DbSet<Donor_Role> Donor_Roles { get; set; }
		public DbSet<Donor> Donors { get; set; }

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			modelBuilder.Entity<Donor_Role>(builder =>
			{
				builder.ToTable("Donor_Roles").HasKey(x => x.Donor_Role_ID);
				builder.Property(x => x.Donor_Role_ID).ValueGeneratedOnAdd();

			});

			modelBuilder.Entity<Donor>(builder =>
			{
				builder.ToTable("Donors").HasKey(x => x.Donor_ID);
				builder.Property(x => x.Donor_ID).ValueGeneratedOnAdd();
				builder.Property(x => x.Login).HasMaxLength(25);
				builder.Property(x => x.Password).HasMaxLength(10);

				builder.HasOne(x => x.Donor_Role)
					.WithMany(x => x.Donors)
					.HasForeignKey(x => x.Donor_Role_ID);

			});
		}
	}
}
