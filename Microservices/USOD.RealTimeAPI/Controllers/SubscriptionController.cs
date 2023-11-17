using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using RealTime_Library;
using USOD.RealTimeAPI.Hubs;
using USOD.RealTimeAPI.Services.Interfaces;

namespace USOD.RealTimeAPI.Controllers
{
	[Route("RealTimeApi/[controller]")]
	[ApiController]
	public class SubscriptionController : ControllerBase
	{
		private readonly ISubscriptionService _subscriptionService;
		private readonly IHubContext<SubscriptionHub, ISubClient> _hubContext;
		private readonly ILogger<SubscriptionController> _logger;

		public SubscriptionController(ISubscriptionService subService, IHubContext<SubscriptionHub, ISubClient> hubContext, ILogger<SubscriptionController> logger)
		{
			_subscriptionService = subService;
			_hubContext = hubContext;
			_logger = logger;
		}

		[HttpGet("{donor_id}")]
		public async Task<IActionResult> Get(int donor_id)
		{
			var sub = await _subscriptionService.GetAsync(donor_id);

			if (!sub.Any()) return NotFound();

			return Ok(sub);
		}

		[HttpGet("GetById/{sub_id}")]
		public async Task<IActionResult> GetById(int sub_id)
		{
			var sub = await _subscriptionService.GetByIdAsync(sub_id);

			if (sub == null) return NotFound();

			return Ok(sub);
		}

		[HttpPost]
		public async Task<IActionResult> Post([FromBody] Subscription sub)
		{
			try
			{
				await _subscriptionService.SubscribeAsync(sub);
				await _hubContext.Clients.Group(sub.Fund_ID.ToString()).ReceiveMessage("Add new project");
				return Ok();
			}
			catch (Exception ex)
			{
				_logger.LogError("Exception: {ex}", ex.Message);
				return BadRequest(ex.Message);
			}
		}

		[HttpDelete("{sub_id}")]
		public async Task<IActionResult> Delete(int sub_id)
		{
			var sub = await _subscriptionService.GetByIdAsync(sub_id);
			if (sub == null) return NotFound();

			try
			{
				await _subscriptionService.UnsubscribeAsync(sub);
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
