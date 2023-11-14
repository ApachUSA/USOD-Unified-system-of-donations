using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Project_Library.Entity;
using USOD.ProjectAPI.Services.Interfaces;

namespace USOD.ProjectAPI.Controllers
{
	[Route("ProjectApi/[controller]")]
	[ApiController]
	public class CardVideoController : ControllerBase
	{
		private readonly ICardVideoService _cardVideoService;
		private readonly ILogger<CardVideoController> _logger;

		public CardVideoController(ICardVideoService cardVideoService, ILogger<CardVideoController> logger)
		{
			_cardVideoService = cardVideoService;
			_logger = logger;
		}


		[HttpGet("{card_id}")]
		public async Task<IActionResult> Get(int card_id)
		{
			var cardVideo = await _cardVideoService.GetAsync(card_id);

			if (!cardVideo.Any()) return NotFound();

			return Ok(cardVideo);
		}

		[HttpGet("GetById/{cardVideo_id}")]
		public async Task<IActionResult> GetById(int cardVideo_id)
		{
			var cardVideo = await _cardVideoService.GetByIdAsync(cardVideo_id);

			if (cardVideo == null) return NotFound();

			return Ok(cardVideo);
		}

		[HttpPost]
		public async Task<IActionResult> Post([FromBody] Card_Video cardVideo)
		{
			try
			{
				await _cardVideoService.CreateAsync(cardVideo);
				return Ok(cardVideo);
			}
			catch (Exception ex)
			{
				_logger.LogError("Exception: {ex}", ex.Message);
				return BadRequest(ex.Message);
			}
		}

		[HttpPost("PostArray")]
		public async Task<IActionResult> PostArray([FromBody] List<Card_Video> cardVideos)
		{
			try
			{
				await _cardVideoService.CreateAsync(cardVideos);
				return Ok(cardVideos);
			}
			catch (Exception ex)
			{
				_logger.LogError("Exception: {ex}", ex.Message);
				return BadRequest(ex.Message);
			}
		}

		[HttpPut]
		public async Task<IActionResult> Put([FromBody] Card_Video cardVideo)
		{
			try
			{
				await _cardVideoService.UpdateAsync(cardVideo);
				return Ok(cardVideo);
			}
			catch (Exception ex)
			{
				_logger.LogError("Exception: {ex}", ex.Message);
				return BadRequest(ex.Message);
			}
		}

		[HttpDelete("{cardVideo_id}")]
		public async Task<IActionResult> Delete(int cardVideo_id)
		{
			var cardVideo = await _cardVideoService.GetByIdAsync(cardVideo_id);
			if (cardVideo == null) return NotFound();

			try
			{
				await _cardVideoService.DeleteAsync(cardVideo);
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
