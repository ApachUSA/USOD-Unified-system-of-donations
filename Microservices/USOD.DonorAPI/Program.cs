using Microsoft.EntityFrameworkCore;
using Serilog;
using Serilog.Events;
using Serilog.Formatting.Compact;
using Serilog.Sinks.RabbitMQ.Sinks.RabbitMQ;
using USOD.DonorAPI.Repositories;
using USOD.DonorAPI.Repositories.Interfaces;
using USOD.DonorAPI.Services.Implementations;
using USOD.DonorAPI.Services.Interfaces;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

builder.Services.AddDbContext<Donor_DB_Context>(options =>
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

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
//builder.Services.AddSwaggerGen();

builder.Services.AddScoped(typeof(IBaseRepository<>), typeof(Repository<>));
builder.Services.AddScoped<IDonorService, DonorService>();
builder.Services.AddScoped<IAuthenticationService, AuthenticationService>();
builder.Services.AddScoped<IDonorRoleService, DonorRoleService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
//if (app.Environment.IsDevelopment())
//{
//	app.UseSwagger();
//	app.UseSwaggerUI();
//}

//app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
