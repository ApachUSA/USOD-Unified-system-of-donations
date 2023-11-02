using Donor_Library.Entity;
using Donor_Library.ViewModel;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using USOD.DonorAPI.Repositories.Interfaces;

namespace USOD.DonorAPI.Controllers
{
    [Route("api/[controller]")]
	[ApiController]
	public class DonorController : ControllerBase
	{
		private readonly IBaseRepository<Donor> _donorRepository;
		private readonly IConfiguration _config;

		public DonorController(IBaseRepository<Donor> donorRepository, IConfiguration config)
		{
			_donorRepository = donorRepository;
			_config = config;
		}

		[HttpPost("Login")]
		public async Task<IActionResult> Login([FromBody] Donor_LoginVM donor_Login)
		{
			var donor = await _donorRepository.Get().FirstOrDefaultAsync( x=> x.Login == donor_Login.Login && x.Password == donor_Login.Password);
			if (donor == null) return NotFound();

			string token = CreateToken(donor);

			return Ok(token);
		}

		[HttpGet("Get")]
		public async Task<IActionResult> Get()
		{
			var donor = await _donorRepository.Get().FirstOrDefaultAsync();

			if (donor == null)return NotFound();
			

			return Ok(donor);
		}

		[HttpPost]
		public async Task<IActionResult> Post([FromBody] Donor donor)
		{
			try
			{
				
				await _donorRepository.Create(donor);
				return Ok(donor);

			}
			catch (Exception ex)
			{

				 return Problem(ex.Message);
			}
			
			
		}


		private string CreateToken(Donor donor)
		{

			List<Claim> claims = new()
			{
				new Claim(ClaimTypes.Name, donor.Surname + donor.Name), 
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
