using Microsoft.AspNetCore.Authentication.Cookies;
using USOD.WebASP.Services.Implementations;
using USOD.WebASP.Services.Interfaces;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews().AddJsonOptions(x => x.JsonSerializerOptions.PropertyNamingPolicy = null);

builder.Services.AddHttpClient("Donor", client => {
	client.BaseAddress = new Uri("http://localhost:5133/DonorApi/");
});
builder.Services.AddHttpClient("Fund", client => {
	client.BaseAddress = new Uri("http://localhost:5133/FundApi/");
});


builder.Services.AddScoped<IDonorService, DonorService>();

builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
	.AddCookie(options =>
	{
		options.LoginPath = new PathString("/Donor/Login");
		options.AccessDeniedPath = new PathString("/Donor/Login");
	});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
	app.UseExceptionHandler("/Home/Error");
	// The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
	app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
	name: "default",
	pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
