using Fund_Library.Entity;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using USOD.FundAPI.Services.Interfaces;

namespace USOD.FundAPI.Controllers
{
	[Route("FundApi/[controller]")]
	[ApiController]
	public class FundImageController : ControllerBase
	{
		private readonly IFundImageService _fundImageService;
		private readonly ILogger<FundImageController> _logger;

		public FundImageController(IFundImageService fundImageService, ILogger<FundImageController> logger)
		{
			_fundImageService = fundImageService;
			_logger = logger;
		}

		[HttpGet("{fund_Id}")]
		public async Task<IActionResult> Get(int fund_Id)
		{
			var fundImage = await _fundImageService.GetAsync(fund_Id);

			if (!fundImage.Any()) return NotFound();

			return Ok(fundImage);
		}

		[HttpGet("GetById/{id}")]
		public async Task<IActionResult> GetById(int id)
		{
			var fundImage = await _fundImageService.GetByIdAsync(id);

			if (fundImage == null) return NotFound();

			return Ok(fundImage);
		}

		[HttpPost]
		public async Task<IActionResult> Post([FromBody] Fund_Image fundImage)
		{
			try
			{
				await _fundImageService.CreateAsync(fundImage);
				return Ok(fundImage);
			}
			catch (Exception ex)
			{
				_logger.LogError("Exception: {ex}", ex.Message);
				return BadRequest(ex.Message);
			}
		}

		[HttpPut]
		public async Task<IActionResult> Put([FromBody] Fund_Image fundImage)
		{
			try
			{
				await _fundImageService.UpdateAsync(fundImage);
				return Ok(fundImage);
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
			var fundImage = await _fundImageService.GetByIdAsync(id);
			if (fundImage == null) return NotFound();

			try
			{
				await _fundImageService.DeleteAsync(fundImage);
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
