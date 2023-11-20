using Azure;
using Donor_Library.Entity;
using Donor_Library.ViewModel;
using Microsoft.IdentityModel.Tokens;
using RabbitMQ.Client;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using USOD.DonorAPI.Services.Interfaces;

namespace USOD.DonorAPI.Services.Implementations
{
	public class AuthenticationService : IAuthenticationService
	{
		private readonly IConfiguration _config;
		private readonly ILogger<AuthenticationService> _logger;

		public AuthenticationService(IConfiguration config, ILogger<AuthenticationService> logger)
		{
			_config = config;
			_logger = logger;
		}

		public async Task<string> AuthenticateAsync(Donor donor)
		{
			return await CreateToken(donor);
		}

		private async Task<string> CreateToken(Donor donor)
		{
			var client = new HttpClient();
			string role = "Donor";

			if (donor.Donor_Role?.Donor_Role_Name == "Admin")
			{
				role = "Admin";
			}
			else
			{
				try
				{
					var response = await client.GetAsync($"http://172.19.0.6:80/FundApi/FundMember/GetByDonor/{donor.Donor_ID}");

					if (response.StatusCode == System.Net.HttpStatusCode.OK)
						role = await response.Content.ReadAsStringAsync();
				}
				catch (Exception ex)
				{
					_logger.LogError("Exception: {ex}", ex.Message);
				}
			}

			List<Claim> claims = new()
			{
				new Claim(ClaimTypes.Name, donor.Login),
				new Claim(ClaimTypes.Role, role),
				new Claim("Role", role)
			};

			var key = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(_config.GetSection("AppSettings:Token").Value));

			var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256Signature);

			var token = new JwtSecurityToken(
				issuer: "https://localhost:7232",
				claims: claims,
				expires: DateTime.UtcNow.AddDays(1),
				signingCredentials: creds
				);

			var jwt = new JwtSecurityTokenHandler().WriteToken(token);

			return jwt;
		}
	}
}
