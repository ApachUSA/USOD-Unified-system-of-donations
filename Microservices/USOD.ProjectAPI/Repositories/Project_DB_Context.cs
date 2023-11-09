using Microsoft.EntityFrameworkCore;
using Project_Library.Entity;

namespace USOD.ProjectAPI.Repositories
{
	public class Project_DB_Context : DbContext
	{
		public Project_DB_Context(DbContextOptions<Project_DB_Context> options) : base(options)
		{
			Database.EnsureCreated();
		}

		public DbSet<Card_Image> Card_Images { get; set; }
		public DbSet<Card_Video> Card_Videos { get; set; }
		public DbSet<Item_Tag> Item_Tags { get; set; }
		public DbSet<Payment_Type> Payment_Types { get; set; }
		public DbSet<Project> Projects { get; set; }
		public DbSet<Project_Card> Project_Cards { get; set; }
		public DbSet<Project_Payment> Project_Payments { get; set; }
		public DbSet<Project_Report> Project_Reports { get; set; }
		public DbSet<Project_Report_Image> Project_Report_Images { get; set; }
		public DbSet<Project_Status> Project_Statuses { get; set; }

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			modelBuilder.Entity<Card_Image>(builder =>
			{
				builder.ToTable("Card_Images").HasKey(x => x.Card_Image_ID);
				builder.Property(x => x.Card_Image_ID).ValueGeneratedOnAdd();

				builder.HasOne(x => x.Project_Card)
					.WithMany(x => x.Card_Images)
					.HasForeignKey(x => x.Project_Card_ID);
			});

			modelBuilder.Entity<Card_Video>(builder =>
			{
				builder.ToTable("Card_Videos").HasKey(x => x.Card_Video_ID);
				builder.Property(x => x.Card_Video_ID).ValueGeneratedOnAdd();

				builder.HasOne(x => x.Project_Card)
					.WithMany(x => x.Card_Videos)
					.HasForeignKey(x => x.Project_Card_ID);
			});

			modelBuilder.Entity<Item_Tag>(builder =>
			{
				builder.ToTable("Item_Tags").HasKey(x => x.Item_Tag_ID);
				builder.Property(x => x.Item_Tag_ID).ValueGeneratedOnAdd();
			});

			modelBuilder.Entity<Payment_Type>(builder =>
			{
				builder.ToTable("Payment_Types").HasKey(x => x.Payment_Type_ID);
				builder.Property(x => x.Payment_Type_ID).ValueGeneratedOnAdd();

			});

			modelBuilder.Entity<Project>(builder =>
			{
				builder.ToTable("Projects").HasKey(x => x.Project_ID);
				builder.Property(x => x.Project_ID).ValueGeneratedOnAdd();

				builder.HasOne(x => x.Project_Status)
					.WithMany(x => x.Projects)
					.HasForeignKey(x => x.Project_Status_ID);

				builder.HasOne(x => x.Project_Report)
					.WithOne(x => x.Project)
					.HasForeignKey<Project_Report>(x => x.Project_ID);
			});

			modelBuilder.Entity<Project_Card>(builder =>
			{
				builder.ToTable("Project_Cards").HasKey(x => x.Project_Card_ID);
				builder.Property(x => x.Project_Card_ID).ValueGeneratedOnAdd();

				builder.HasOne(x => x.Project)
					.WithMany(x => x.Project_Cards)
					.HasForeignKey(x => x.Project_ID);

				builder.HasOne(x => x.Item_Tag)
					.WithMany(x => x.Cards)
					.HasForeignKey(x => x.Item_Tag_ID);
			});

			modelBuilder.Entity<Project_Payment>(builder =>
			{
				builder.ToTable("Project_Payment").HasKey(x => x.Project_Payment_ID);
				builder.Property(x => x.Project_Payment_ID).ValueGeneratedOnAdd();

				builder.HasOne(x => x.Payment_Type)
					.WithMany(x => x.Project_Payments)
					.HasForeignKey(x => x.Payment_Type_ID);
			});

			modelBuilder.Entity<Project_Report>(builder =>
			{
				builder.ToTable("Project_Reports").HasKey(x => x.Project_Report_ID);
				builder.Property(x => x.Project_Report_ID).ValueGeneratedOnAdd();

				builder.HasOne(x => x.Project)
					.WithOne(x => x.Project_Report)
					.HasForeignKey<Project>(x => x.Project_Report_ID);
			});

			modelBuilder.Entity<Project_Report_Image>(builder =>
			{
				builder.ToTable("Project_Report_Images").HasKey(x => x.Project_Report_Image_ID);
				builder.Property(x => x.Project_Report_Image_ID).ValueGeneratedOnAdd();

				builder.HasOne(x => x.Project_Report)
					.WithMany(x => x.Images)
					.HasForeignKey(x => x.Project_Report_ID);
			});

			modelBuilder.Entity<Project_Status>(builder =>
			{
				builder.ToTable("Project_Statuses").HasKey(x => x.Project_Status_ID);
				builder.Property(x => x.Project_Status_ID).ValueGeneratedOnAdd();
			});


		}

	}
}
