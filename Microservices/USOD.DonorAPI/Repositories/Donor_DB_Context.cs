using Donor_Library.Entity;
using Microsoft.EntityFrameworkCore;
using System;

namespace USOD.DonorAPI.Repositories
{
	public class Donor_DB_Context : DbContext
	{
		public Donor_DB_Context(DbContextOptions<Donor_DB_Context> options) : base(options)
		{
			Database.EnsureCreated();
		}

		public DbSet<Donor_Role> Donor_Roles { get; set; }
		public DbSet<Donor> Donors { get; set; }
		public DbSet<Donor_Media_Type> Donor_Media_Types { get; set; }
		public DbSet<Donor_Media> Donor_Medias { get; set; }

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

			modelBuilder.Entity<Donor_Media_Type>(builder =>
			{
				builder.ToTable("Donor_Media_Types").HasKey(x => x.Donor_Media_Type_ID);
				builder.Property(x => x.Donor_Media_Type_ID).ValueGeneratedOnAdd();

			});

			modelBuilder.Entity<Donor_Media>(builder =>
			{
				builder.ToTable("Donor_Media").HasKey(x => x.Donor_Media_ID);
				builder.Property(x => x.Donor_Media_ID).ValueGeneratedOnAdd();

				builder.HasOne(x => x.Media_Type)
					.WithMany(x => x.Donor_Medias)
					.HasForeignKey(x => x.Donor_Media_Type_ID);

				builder.HasOne(x => x.Donor)
					.WithMany(x => x.Donor_Medias)
					.HasForeignKey(x => x.Donor_ID);

			});

		}
	}
}
