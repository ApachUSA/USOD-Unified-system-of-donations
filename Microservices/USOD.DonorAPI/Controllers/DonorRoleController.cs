using Donor_Library.Entity;
using Donor_Library.ViewModel;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using USOD.DonorAPI.Repositories.Interfaces;
using USOD.DonorAPI.Services.Implementations;
using USOD.DonorAPI.Services.Interfaces;

namespace USOD.DonorAPI.Controllers
{
    [Route("DonorApi/[controller]")]
	[ApiController]
	public class DonorRoleController : ControllerBase
	{
		private readonly IDonorRoleService _donorRoleService;
		private readonly ILogger<DonorRoleController> _logger;

		public DonorRoleController(IDonorRoleService donorRoleService, ILogger<DonorRoleController> logger)
		{
			_donorRoleService = donorRoleService;
			_logger = logger;
		}

		[HttpGet]
		public async Task<IActionResult> Get()
		{
			var donorRoles = await _donorRoleService.GetAsync();

			if (!donorRoles.Any()) return NotFound();

			return Ok(donorRoles);
		}

		[HttpGet("{id}")]
		public async Task<IActionResult> Get(int id)
		{
			var donorRoles = await _donorRoleService.GetByIDAsync(id);

			if (donorRoles == null) return NotFound();

			return Ok(donorRoles);
		}

		[HttpPost]
		public async Task<IActionResult> Post([FromBody] Donor_Role donorRole)
		{
			try
			{
				await _donorRoleService.CreateAsync(donorRole);
				return Ok(donorRole);
			}
			catch (Exception ex)
			{
				_logger.LogError("Exception: {ex}", ex.Message);
				return BadRequest(ex.Message);
			}
		}

		[HttpPut]
		public async Task<IActionResult> Put([FromBody] Donor_Role donorRole)
		{
			try
			{
				await _donorRoleService.UpdateAsync(donorRole);
				return Ok(donorRole);
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
			var donor = await _donorRoleService.GetByIDAsync(id);
			if (donor == null) return NotFound();

			try
			{
				await _donorRoleService.DeleteAsync(donor);
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
