using Microsoft.EntityFrameworkCore;
using USOD.RealTimeAPI.Hubs;
using USOD.RealTimeAPI.Repositories;
using USOD.RealTimeAPI.Repositories.Interfaces;
using USOD.RealTimeAPI.Services.Implementations;
using USOD.RealTimeAPI.Services.Interfaces;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddDbContext<RealTime_DB_Context>(options =>
						options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//builder.Services.AddHostedService<RabbitMqConsumer>();

builder.Services.AddScoped(typeof(IBaseRepository<>), typeof(Repository<>));
builder.Services.AddScoped<IProjectCommentService, ProjectCommentService>();
builder.Services.AddScoped<ISubscriptionService, SubscriptionService>();

builder.Services.AddSignalR();

var app = builder.Build();
app.UseWebSockets();
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
	app.UseSwagger();
	app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.MapHub<CommentHub>("comment-hub");
app.MapHub<SubscriptionHub>("subscription-hub");

app.UseAuthorization();

app.MapControllers();

app.Run();
