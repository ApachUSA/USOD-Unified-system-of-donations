using Microsoft.EntityFrameworkCore;
using System.Text.Json.Serialization;
using USOD.ProjectAPI.Repositories;
using USOD.ProjectAPI.Repositories.Interfaces;
using USOD.ProjectAPI.Services.Implementations;
using USOD.ProjectAPI.Services.Interfaces;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers().AddJsonOptions(x =>
				x.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles);

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

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
	app.UseSwagger();
	app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
