using Microsoft.EntityFrameworkCore;
using RealTime_Library;

namespace USOD.RealTimeAPI.Repositories
{
	public class RealTime_DB_Context : DbContext
	{
		public RealTime_DB_Context(DbContextOptions<RealTime_DB_Context> options) : base(options)
		{
			Database.EnsureCreated();
		}

		public DbSet<Project_Comment> Project_Comments { get; set; }
		public DbSet<Subscription> Subscriptions { get; set; }

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			modelBuilder.Entity<Project_Comment>(builder =>
			{
				builder.ToTable("Project_Comments").HasKey(x => x.Comment_ID);
				builder.Property(x => x.Comment_ID).ValueGeneratedOnAdd();
			});

			modelBuilder.Entity<Subscription>(builder =>
			{
				builder.ToTable("Subscriptions").HasKey(x => x.Subscription_ID);
				builder.Property(x => x.Subscription_ID).ValueGeneratedOnAdd();
			});
		}
	}
}
