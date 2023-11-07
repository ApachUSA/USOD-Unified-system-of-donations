using Donor_Library.Entity;
using Donor_Library.ViewModel;
using Microsoft.AspNetCore.Mvc;
using USOD.DonorAPI.Services.Interfaces;

namespace USOD.DonorAPI.Controllers
{
	[Route("DonorApi/[controller]")]
	[ApiController]
	public class DonorController : ControllerBase
	{
		private readonly IDonorService _donorService;
		private readonly IAuthenticationService _authenticationService;
		private readonly ILogger<Donor> _logger;

		public DonorController(IDonorService donorService, IAuthenticationService authenticationService, ILogger<Donor> logger)
		{
			_donorService = donorService;
			_authenticationService = authenticationService;
			_logger = logger;
		}

		[HttpPost("Login")]
		public async Task<IActionResult> Login([FromBody] Donor_LoginVM donor_Login)
		{
			var donor = await _donorService.GetByLoginAsync(donor_Login);

			if (donor == null) return NotFound();

			

			return Ok(await _authenticationService.AuthenticateAsync(donor));
		}


		[HttpGet]
		public async Task<IActionResult> Get()
		{
			var donor = await _donorService.GetAsync();

			if (!donor.Any()) return NotFound();

			return Ok(donor);
		}

		[HttpGet("{id}")]
		public async Task<IActionResult> Get(int id)
		{
			var donor = await _donorService.GetByIDAsync(id);

			if (donor == null) return NotFound();

			return Ok(donor);
		}

		[HttpPost]
		public async Task<IActionResult> Post([FromBody] Donor donor)
		{
			if( await _donorService.CheckUsername(donor.Username)) 
				return Conflict(new ProblemDetails() { Detail = "The username is already in use." });
			try
			{
				await _donorService.RegisterAsync(donor);
				return Ok(donor);
			}
			catch (Exception ex)
			{
				_logger.LogError("Exception: {ex}", ex.Message);
				 return BadRequest(ex.Message);
			}
		}

		[HttpPut]
		public async Task<IActionResult> Put([FromBody] Donor donor)
		{
			try
			{
				await _donorService.UpdateAsync(donor);
				return Ok(donor);
			}
			catch (Exception ex)
			{
				_logger.LogError("Exception: {ex}", ex.Message);
				return BadRequest(ex.Message);
			}
		}

		[HttpDelete("{id}")]
		public async Task<IActionResult> Delete(int id)
		{
			var donor = await _donorService.GetByIDAsync(id);
			if (donor == null) return NotFound();

			try
			{
				await _donorService.DeleteAsync(donor);
				return Ok();
			}
			catch (Exception ex)
			{
				_logger.LogError("Exception: {ex}", ex.Message);
				return BadRequest(ex.Message);
			}
		}
	}
}
