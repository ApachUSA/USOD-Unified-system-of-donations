using Microsoft.EntityFrameworkCore;
using USOD.FundAPI.Repositories;
using USOD.FundAPI.Repositories.Interfaces;
using USOD.FundAPI.Services.Implementations;
using USOD.FundAPI.Services.Interfaces;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

builder.Services.AddDbContext<Fund_DB_Context>(options =>
						options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddScoped(typeof(IBaseRepository<>), typeof(Repository<>));
builder.Services.AddScoped<IFundImageService, FundImageService>();
builder.Services.AddScoped<IFundMediaService, FundMediaService>();
builder.Services.AddScoped<IFundMemberService, FundMemberService>();
builder.Services.AddScoped<IFundService, FundService>();
builder.Services.AddScoped<IMediaTypeService, MediaTypeService>();
builder.Services.AddScoped<IMemberRoleService, MemberRoleService>();


builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

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
