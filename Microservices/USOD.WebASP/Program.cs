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
	client.BaseAddress = new Uri("http://localhost:5103/FundApi/");
});

builder.Services.AddHttpClient("Project", client => {
	client.BaseAddress = new Uri("http://localhost:5096/ProjectApi/");
});

builder.Services.AddHttpClient("RealTime", client => {
	client.BaseAddress = new Uri("http://localhost:5261/RealTimeApi/");
});


builder.Services.AddScoped<IDonorService, DonorService>();

builder.Services.AddScoped<IFundService, FundService>();
builder.Services.AddScoped<IFundImageService, FundImageService>();
builder.Services.AddScoped<IFundMemberService, FundMemberService>();
builder.Services.AddScoped<IFundMemberRoleService, FundMemberRoleService>();
builder.Services.AddScoped<IFundSubscriptionService, FundSubscriptionService>();

builder.Services.AddScoped<IProjectService, ProjectService>();
builder.Services.AddScoped<IPaymentTypeService, PaymentTypeService>();
builder.Services.AddScoped<IProjectFundService, ProjectFundService>();
builder.Services.AddScoped<IReportImageService, ReportImageService>();
builder.Services.AddScoped<IProjectCommentService, ProjectCommentService>();

builder.Services.AddScoped<IProjectCardService, ProjectCardService>();
builder.Services.AddScoped<ICardImageService, CardImageService>();
builder.Services.AddScoped<ICardVideoService, CardVideoService>();
builder.Services.AddScoped<IItemTagService, ItemTagService>();



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
