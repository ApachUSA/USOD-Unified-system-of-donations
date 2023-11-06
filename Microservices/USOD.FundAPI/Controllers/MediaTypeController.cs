using Fund_Library.Entity;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using USOD.FundAPI.Services.Interfaces;

namespace USOD.FundAPI.Controllers
{
	[Route("FundApi/[controller]")]
	[ApiController]
	public class MediaTypeController : ControllerBase
	{
		private readonly IMediaTypeService _mediaTypeService;
		private readonly ILogger<MediaTypeController> _logger;

		public MediaTypeController(IMediaTypeService mediaTypeService, ILogger<MediaTypeController> logger)
		{
			_mediaTypeService = mediaTypeService;
			_logger = logger;
		}

		[HttpGet]
		public async Task<IActionResult> Get()
		{
			var mediaType = await _mediaTypeService.GetAsync();

			if (!mediaType.Any()) return NotFound();

			return Ok(mediaType);
		}

		[HttpGet("{id}")]
		public async Task<IActionResult> Get(int id)
		{
			var mediaType = await _mediaTypeService.GetByIdAsync(id);

			if (mediaType == null) return NotFound();

			return Ok(mediaType);
		}

		[HttpPost]
		public async Task<IActionResult> Post([FromBody] Media_Type mediaType)
		{
			try
			{
				await _mediaTypeService.CreateAsync(mediaType);
				return Ok(mediaType);
			}
			catch (Exception ex)
			{
				_logger.LogError("Exception: {ex}", ex.Message);
				return BadRequest(ex.Message);
			}
		}

		[HttpPut]
		public async Task<IActionResult> Put([FromBody] Media_Type mediaType)
		{
			try
			{
				await _mediaTypeService.UpdateAsync(mediaType);
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
			var mediaType = await _mediaTypeService.GetByIdAsync(id);
			if (mediaType == null) return NotFound();

			try
			{
				await _mediaTypeService.DeleteAsync(mediaType);
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
