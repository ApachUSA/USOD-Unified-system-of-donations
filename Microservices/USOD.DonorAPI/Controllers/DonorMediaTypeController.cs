using Donor_Library.Entity;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using USOD.DonorAPI.Services.Interfaces;

namespace USOD.DonorAPI.Controllers
{
	[Route("DonorApi/[controller]")]
	[ApiController]
	public class DonorMediaTypeController : ControllerBase
	{
		private readonly IDonorMediaTypeService _donorMediaTypeService;
		private readonly ILogger<DonorMediaTypeController> _logger;

		public DonorMediaTypeController(IDonorMediaTypeService donorMediaTypeService, ILogger<DonorMediaTypeController> logger)
		{
			_donorMediaTypeService = donorMediaTypeService;
			_logger = logger;
		}

		[HttpGet]
		public async Task<IActionResult> Get()
		{
			var mediaTypes = await _donorMediaTypeService.GetAsync();

			if (!mediaTypes.Any()) return NotFound();

			return Ok(mediaTypes);
		}

		[HttpGet("{id}")]
		public async Task<IActionResult> Get(int id)
		{
			var mediaTypes = await _donorMediaTypeService.GetByIDAsync(id);

			if (mediaTypes == null) return NotFound();

			return Ok(mediaTypes);
		}

		[HttpPost]
		public async Task<IActionResult> Post([FromBody] Donor_Media_Type mediaType)
		{
			try
			{
				await _donorMediaTypeService.CreateAsync(mediaType);
				return Ok(mediaType);
			}
			catch (Exception ex)
			{
				_logger.LogError("Exception: {ex}", ex.Message);
				return BadRequest(ex.Message);
			}
		}

		[HttpPut]
		public async Task<IActionResult> Put([FromBody] Donor_Media_Type mediaType)
		{
			try
			{
				await _donorMediaTypeService.UpdateAsync(mediaType);
				return Ok(mediaType);
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
			var mediaType = await _donorMediaTypeService.GetByIDAsync(id);
			if (mediaType == null) return NotFound();

			try
			{
				await _donorMediaTypeService.DeleteAsync(mediaType);
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
