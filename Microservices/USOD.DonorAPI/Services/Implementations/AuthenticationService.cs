using Donor_Library.Entity;
using Donor_Library.ViewModel;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using USOD.DonorAPI.Services.Interfaces;

namespace USOD.DonorAPI.Services.Implementations
{
	public class AuthenticationService : IAuthenticationService
	{
		private readonly IConfiguration _config;

		public AuthenticationService(IConfiguration config)
		{
			_config = config;
		}

		public string AuthenticateAsync(Donor donor)
		{
			return CreateToken(donor);
		}

		private string CreateToken(Donor donor)
		{
			List<Claim> claims = new()
			{
				new Claim(ClaimTypes.Name, donor.Username),
				new Claim(ClaimTypes.Role, donor.Donor_Role == null? "User" :  donor.Donor_Role.Donor_Role_Name),
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
