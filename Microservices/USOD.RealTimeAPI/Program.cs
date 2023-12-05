using Microsoft.EntityFrameworkCore;
using Serilog;
using Serilog.Events;
using Serilog.Formatting.Compact;
using Serilog.Sinks.RabbitMQ.Sinks.RabbitMQ;
using System.Text.Json.Serialization;
using USOD.RealTimeAPI.Hubs;
using USOD.RealTimeAPI.Repositories;
using USOD.RealTimeAPI.Repositories.Interfaces;
using USOD.RealTimeAPI.Services.Implementations;
using USOD.RealTimeAPI.Services.Interfaces;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddDbContext<RealTime_DB_Context>(options =>
						options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Host.UseSerilog((context, configuration) =>
	configuration.WriteTo.RabbitMQ((clientConfiguration, sinkConfiguration) =>
	{
		clientConfiguration.From(builder.Configuration.GetSection("RabbitMQ").Get<RabbitMQClientConfiguration>());
		sinkConfiguration.TextFormatter = new CompactJsonFormatter();
	})
	  .MinimumLevel.Information()
	  .MinimumLevel.Override("Microsoft", LogEventLevel.Warning)
	  .MinimumLevel.Override("System", LogEventLevel.Warning)
);

builder.Services.AddControllers().AddJsonOptions(x =>
				x.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles).AddJsonOptions(x => x.JsonSerializerOptions.PropertyNamingPolicy = null);

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddHostedService<RabbitMqConsumer>();

builder.Services.AddScoped(typeof(IBaseRepository<>), typeof(Repository<>));
builder.Services.AddScoped<IProjectCommentService, ProjectCommentService>();
builder.Services.AddScoped<ISubscriptionService, SubscriptionService>();

builder.Services.AddSignalR().AddJsonProtocol(options =>
{
	options.PayloadSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
	options.PayloadSerializerOptions.PropertyNamingPolicy = null;
});

builder.Services.AddCors(options =>
{
	options.AddDefaultPolicy(
		builder =>
		{
			builder
				.WithOrigins("https://localhost:7208")  // Add the actual origin of your client application
				.AllowAnyHeader()
				.AllowAnyMethod()
				.AllowCredentials();
		});
});

var app = builder.Build();
app.UseWebSockets();
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
	app.UseSwagger();
	app.UseSwaggerUI();
}

//app.UseHttpsRedirection();
app.UseCors();

app.MapHub<CommentHub>("comment-hub");
app.MapHub<SubscriptionHub>("subscription-hub");

app.UseAuthorization();

app.MapControllers();

app.Run();
