using Fund_Library.Entity;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using USOD.FundAPI.Services.Interfaces;

namespace USOD.FundAPI.Controllers
{
	[Route("FundApi/[controller]")]
	[ApiController]
	public class FundMediaController : ControllerBase
	{
		private readonly IFundMediaService _fundMediaService;
		private readonly ILogger<FundMediaController> _logger;

		public FundMediaController(IFundMediaService fundMediaService, ILogger<FundMediaController> logger)
		{
			_fundMediaService = fundMediaService;
			_logger = logger;
		}

		[HttpGet("{id}")]
		public async Task<IActionResult> Get(int fund_Id)
		{
			var fundMedia = await _fundMediaService.GetAsync(fund_Id);

			if (!fundMedia.Any()) return NotFound();

			return Ok(fundMedia);
		}

		[HttpGet("GetById/{id}")]
		public async Task<IActionResult> GetById(int id)
		{
			var fundImage = await _fundMediaService.GetByIdAsync(id);

			if (fundImage == null) return NotFound();

			return Ok(fundImage);
		}

		[HttpPost]
		public async Task<IActionResult> Post([FromBody] Fund_Media fundMedia)
		{
			try
			{
				await _fundMediaService.CreateAsync(fundMedia);
				return Ok(fundMedia);
			}
			catch (Exception ex)
			{
				_logger.LogError("Exception: {ex}", ex.Message);
				return BadRequest(ex.Message);
			}
		}

		[HttpPut]
		public async Task<IActionResult> Put([FromBody] Fund_Media fundMedia)
		{
			try
			{
				await _fundMediaService.UpdateAsync(fundMedia);
				return Ok(fundMedia);
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
			var fundMedia = await _fundMediaService.GetByIdAsync(id);
			if (fundMedia == null) return NotFound();

			try
			{
				await _fundMediaService.DeleteAsync(fundMedia);
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
