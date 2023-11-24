using Donor_Library.Entity;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using USOD.DonorAPI.Services.Interfaces;

namespace USOD.DonorAPI.Controllers
{
	[Route("DonorApi/[controller]")]
	[ApiController]
	public class DonorMediaController : ControllerBase
	{
		private readonly IDonorMediaService _donorMediaService;
		private readonly ILogger<DonorMediaController> _logger;

		public DonorMediaController(IDonorMediaService donorMediaService, ILogger<DonorMediaController> logger)
		{
			_donorMediaService = donorMediaService;
			_logger = logger;
		}

		[HttpGet]
		public async Task<IActionResult> Get()
		{
			var donorMedias = await _donorMediaService.GetAsync();

			if (!donorMedias.Any()) return NotFound();

			return Ok(donorMedias);
		}

		[HttpGet("{id}")]
		public async Task<IActionResult> Get(int id)
		{
			var donorMedias = await _donorMediaService.GetByIDAsync(id);

			if (donorMedias == null) return NotFound();

			return Ok(donorMedias);
		}

		[HttpPost]
		public async Task<IActionResult> Post([FromBody] Donor_Media donorMedia)
		{
			try
			{
				await _donorMediaService.CreateAsync(donorMedia);
				return Ok(donorMedia);
			}
			catch (Exception ex)
			{
				_logger.LogError("Exception: {ex}", ex.Message);
				return BadRequest(ex.Message);
			}
		}

		[HttpPost("PostArray")]
		public async Task<IActionResult> PostArray([FromBody] List<Donor_Media> donorMedias)
		{
			try
			{
				await _donorMediaService.CreateAsync(donorMedias);
				return Ok(donorMedias);
			}
			catch (Exception ex)
			{
				_logger.LogError("Exception: {ex}", ex.Message);
				return BadRequest(ex.Message);
			}
		}

		[HttpPut]
		public async Task<IActionResult> Put([FromBody] Donor_Media donorMedia)
		{
			try
			{
				await _donorMediaService.UpdateAsync(donorMedia);
				return Ok(donorMedia);
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
			var donorMedia = await _donorMediaService.GetByIDAsync(id);
			if (donorMedia == null) return NotFound();

			try
			{
				await _donorMediaService.DeleteAsync(donorMedia);
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
