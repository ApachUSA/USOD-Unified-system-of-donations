using Fund_Library.Entity;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using USOD.FundAPI.Services.Interfaces;

namespace USOD.FundAPI.Controllers
{
	[Route("FundApi/[controller]")]
	[ApiController]
	public class FundController : ControllerBase
	{
		private readonly IFundService _fundService;
		private readonly ILogger<FundController> _logger;

		public FundController(IFundService fundService, ILogger<FundController> logger)
		{
			_fundService = fundService;
			_logger = logger;
		}

		[HttpGet]
		public async Task<IActionResult> Get()
		{
			var fund = await _fundService.GetAsync();

			if (!fund.Any()) return NotFound();

			return Ok(fund);
		}

		[HttpGet("{id}")]
		public async Task<IActionResult> GetById(int id)
		{
			var fund = await _fundService.GetByIdAsync(id);

			if (fund == null) return NotFound();

			return Ok(fund);
		}

		[HttpPost]
		public async Task<IActionResult> Post([FromBody] Fund fund)
		{
			try
			{
				await _fundService.CreateAsync(fund);
				return Ok(fund);
			}
			catch (Exception ex)
			{
				_logger.LogError("Exception: {ex}", ex.Message);
				return BadRequest(ex.Message);
			}
		}

		[HttpPut]
		public async Task<IActionResult> Put([FromBody] Fund fund)
		{
			try
			{
				await _fundService.UpdateAsync(fund);
				return Ok(fund);
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
			var fund = await _fundService.GetByIdAsync(id);
			if (fund == null) return NotFound();

			try
			{
				await _fundService.DeleteAsync(fund);
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
