using Fund_Library.Entity;
using Microsoft.EntityFrameworkCore;

namespace USOD.FundAPI.Repositories
{
	public class Fund_DB_Context : DbContext
	{
		public Fund_DB_Context(DbContextOptions<Fund_DB_Context> options) : base(options)
		{
			Database.EnsureCreated();
		}

		public DbSet<Fund> Funds { get; set; }
		public DbSet<Fund_Image> Fund_Images { get; set; }
		public DbSet<Fund_Media> Fund_Medias { get; set; }
		public DbSet<Fund_Member> Fund_Members { get; set; }
		public DbSet<Media_Type> Media_Types { get; set; }
		public DbSet<Member_Role> Member_Roles { get; set; }
		

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			modelBuilder.Entity<Fund>(builder =>
			{
				builder.ToTable("Funds").HasKey(x => x.Fund_ID);
				builder.Property(x => x.Fund_ID).ValueGeneratedOnAdd();
			});

			modelBuilder.Entity<Fund_Image>(builder =>
			{
				builder.ToTable("Fund_Images").HasKey(x => x.Fund_Image_ID);
				builder.Property(x => x.Fund_Image_ID).ValueGeneratedOnAdd();

				builder.HasOne(x => x.Fund)
					.WithMany(x => x.Fund_Images)
					.HasForeignKey(x => x.Fund_ID);
			});

			modelBuilder.Entity<Fund_Media>(builder =>
			{
				builder.ToTable("Fund_Medias").HasKey(x => x.Fund_Media_ID);
				builder.Property(x => x.Fund_Media_ID).ValueGeneratedOnAdd();

				builder.HasOne(x => x.Fund)
					.WithMany(x => x.Fund_Medias)
					.HasForeignKey(x => x.Fund_ID);

				builder.HasOne(x => x.Media_Type)
					.WithMany(x => x.Fund_Medias)
					.HasForeignKey(x => x.Media_Type_ID);
			});

			modelBuilder.Entity<Fund_Member>(builder =>
			{
				builder.ToTable("Fund_Members").HasKey(x => x.Fund_Member_ID);
				builder.Property(x => x.Fund_Member_ID).ValueGeneratedOnAdd();

				builder.HasOne(x => x.Fund)
					.WithMany(x => x.Fund_Members)
					.HasForeignKey(x => x.Fund_ID);

				builder.HasOne(x => x.Member_Role)
					.WithMany(x => x.Fund_Members)
					.HasForeignKey(x => x.Member_Role_ID);
			});


			modelBuilder.Entity<Media_Type>(builder =>
			{
				builder.ToTable("Media_Types").HasKey(x => x.Media_Type_ID);
				builder.Property(x => x.Media_Type_ID).ValueGeneratedOnAdd();
			});

			modelBuilder.Entity<Member_Role>(builder =>
			{
				builder.ToTable("Member_Roles").HasKey(x => x.Member_Role_ID);
				builder.Property(x => x.Member_Role_ID).ValueGeneratedOnAdd();
			});




		}
	}
}
