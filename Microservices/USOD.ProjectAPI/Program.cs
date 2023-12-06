using Microsoft.EntityFrameworkCore;
using Serilog;
using Serilog.Events;
using Serilog.Formatting.Compact;
using Serilog.Sinks.RabbitMQ.Sinks.RabbitMQ;
using System.Text.Json.Serialization;
using USOD.ProjectAPI.Repositories;
using USOD.ProjectAPI.Repositories.Interfaces;
using USOD.ProjectAPI.Services.Implementations;
using USOD.ProjectAPI.Services.Interfaces;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers().AddJsonOptions(x =>
				x.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles).AddJsonOptions(x => x.JsonSerializerOptions.PropertyNamingPolicy = null);

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

builder.Services.AddDbContext<Project_DB_Context>(options =>
						options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddScoped(typeof(IBaseRepository<>), typeof(Repository<>));
builder.Services.AddScoped<ICardImageService, CardImageService>();
builder.Services.AddScoped<ICardVideoService, CardVideoService>();
builder.Services.AddScoped<IItemTagService, ItemTagService>();
builder.Services.AddScoped<IPaymentTypeService, PaymentTypeService>();
builder.Services.AddScoped<IProjectCardService, ProjectCardService>();
builder.Services.AddScoped<IProjectPaymentService, ProjectPaymentService>();
builder.Services.AddScoped<IProjectReportImageService, ProjectReportImageService>();
builder.Services.AddScoped<IProjectReportService, ProjectReportService>();
builder.Services.AddScoped<IProjectFundService, ProjectFundService>();
builder.Services.AddScoped<IProjectService, ProjectService>();
builder.Services.AddScoped<IProjectStatusService, ProjectStatusService>();

builder.Services.AddScoped<IMessageProducer, RabbitMQProducer>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
	app.UseSwagger();
	app.UseSwaggerUI();
}

//app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
