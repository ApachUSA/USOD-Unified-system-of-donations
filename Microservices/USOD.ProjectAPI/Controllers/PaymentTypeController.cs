using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Project_Library.Entity;
using USOD.ProjectAPI.Services.Interfaces;

namespace USOD.ProjectAPI.Controllers
{
	[Route("ProjectApi/[controller]")]
	[ApiController]
	public class PaymentTypeController : ControllerBase
	{
		private readonly IPaymentTypeService _paymentTypeService;
		private readonly ILogger<PaymentTypeController> _logger;

		public PaymentTypeController(IPaymentTypeService paymentTypeService, ILogger<PaymentTypeController> logger)
		{
			_paymentTypeService = paymentTypeService;
			_logger = logger;
		}


		[HttpGet]
		public async Task<IActionResult> Get()
		{
			var paymentTypes = await _paymentTypeService.GetAsync();

			if (!paymentTypes.Any()) return NotFound();

			return Ok(paymentTypes);
		}

		[HttpGet("GetById/{paymentType_id}")]
		public async Task<IActionResult> GetById(int paymentType_id)
		{
			var paymentType = await _paymentTypeService.GetByIdAsync(paymentType_id);

			if (paymentType == null) return NotFound();

			return Ok(paymentType);
		}

		[HttpPost]
		public async Task<IActionResult> Post([FromBody] Payment_Type paymentType)
		{
			try
			{
				await _paymentTypeService.CreateAsync(paymentType);
				return Ok(paymentType);
			}
			catch (Exception ex)
			{
				_logger.LogError("Exception: {ex}", ex.Message);
				return BadRequest(ex.Message);
			}
		}

		[HttpPut]
		public async Task<IActionResult> Put([FromBody] Payment_Type paymentType)
		{
			try
			{
				await _paymentTypeService.UpdateAsync(paymentType);
				return Ok(paymentType);
			}
			catch (Exception ex)
			{
				_logger.LogError("Exception: {ex}", ex.Message);
				return BadRequest(ex.Message);
			}
		}

		[HttpDelete("{paymentType_id}")]
		public async Task<IActionResult> Delete(int paymentType_id)
		{
			var paymentType = await _paymentTypeService.GetByIdAsync(paymentType_id);
			if (paymentType == null) return NotFound();

			try
			{
				await _paymentTypeService.DeleteAsync(paymentType);
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
