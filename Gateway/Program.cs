using Ocelot.DependencyInjection;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Ocelot.Middleware;
using Serilog;
using Serilog.Events;
using Serilog.Formatting.Compact;
using Serilog.Sinks.RabbitMQ.Sinks.RabbitMQ;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using USOD.ApiGateway;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
//builder.Services.AddSwaggerGen();

builder.Configuration.AddJsonFile("ocelot.json", false, false);
builder.Services.AddOcelot(builder.Configuration);

builder.Host.UseSerilog((context, configuration) =>
	configuration.WriteTo.RabbitMQ((clientConfiguration, sinkConfiguration) =>
	{
		clientConfiguration.From(builder.Configuration.GetSection("RabbitMQ").Get<RabbitMQClientConfiguration>());
		sinkConfiguration.TextFormatter = new CompactJsonFormatter();
	})
	  .MinimumLevel.Information()
	  .MinimumLevel.Override("Microsoft", LogEventLevel.Warning)
	  .MinimumLevel.Override("Ocelot", LogEventLevel.Warning)
	  .MinimumLevel.Override("System", LogEventLevel.Warning)
);

builder.Services.AddAuthentication(opt => {
	opt.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
	opt.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
		.AddJwtBearer(options =>
		{
			options.TokenValidationParameters = new TokenValidationParameters
			{
				ValidateIssuer = true,
				ValidIssuer = "https://localhost:7232",
				ValidateAudience = false,
				ValidateIssuerSigningKey = true,
				IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration.GetSection("AppSettings:Token").Value))
			};
		});

var app = builder.Build();

app.UseWebSockets();

await app.UseOcelot(new OcelotPipelineConfiguration
{
	AuthorizationMiddleware = OcelotAuthorizationMiddleware.Authorize
});

// Configure the HTTP request pipeline.
//if (app.Environment.IsDevelopment())
//{
//	app.UseSwagger();
//	app.UseSwaggerUI();
//}

app.UseAuthentication();
app.UseAuthorization();

//app.UseHttpsRedirection();

app.Run();

