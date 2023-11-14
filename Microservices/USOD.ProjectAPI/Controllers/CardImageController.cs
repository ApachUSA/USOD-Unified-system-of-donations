using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Project_Library.Entity;
using USOD.ProjectAPI.Services.Interfaces;

namespace USOD.ProjectAPI.Controllers
{
	[Route("ProjectApi/[controller]")]
	[ApiController]
	public class CardImageController : ControllerBase
	{
		private readonly ICardImageService _cardImageService;
		private readonly ILogger<CardImageController> _logger;

		public CardImageController(ICardImageService cardImageService, ILogger<CardImageController> logger)
		{
			_cardImageService = cardImageService;
			_logger = logger;
		}


		[HttpGet("{card_id}")]
		public async Task<IActionResult> Get(int card_id)
		{
			var cardImage = await _cardImageService.GetAsync(card_id);

			if (!cardImage.Any()) return NotFound();

			return Ok(cardImage);
		}

		[HttpGet("GetById/{cardImage_id}")]
		public async Task<IActionResult> GetById(int cardImage_id)
		{
			var cardImage = await _cardImageService.GetByIdAsync(cardImage_id);

			if (cardImage == null) return NotFound();

			return Ok(cardImage);
		}

		[HttpPost]
		public async Task<IActionResult> Post([FromBody] Card_Image cardImage)
		{
			try
			{
				await _cardImageService.CreateAsync(cardImage);
				return Ok(cardImage);
			}
			catch (Exception ex)
			{
				_logger.LogError("Exception: {ex}", ex.Message);
				return BadRequest(ex.Message);
			}
		}

		[HttpPost("PostArray")]
		public async Task<IActionResult> PostArray([FromBody] List<Card_Image> cardImages)
		{
			try
			{
				await _cardImageService.CreateAsync(cardImages);
				return Ok(cardImages);
			}
			catch (Exception ex)
			{
				_logger.LogError("Exception: {ex}", ex.Message);
				return BadRequest(ex.Message);
			}
		}

		[HttpPut]
		public async Task<IActionResult> Put([FromBody] Card_Image cardImage)
		{
			try
			{
				await _cardImageService.UpdateAsync(cardImage);
				return Ok(cardImage);
			}
			catch (Exception ex)
			{
				_logger.LogError("Exception: {ex}", ex.Message);
				return BadRequest(ex.Message);
			}
		}

		[HttpDelete("{cardImage_id}")]
		public async Task<IActionResult> Delete(int cardImage_id)
		{
			var cardImage = await _cardImageService.GetByIdAsync(cardImage_id);
			if (cardImage == null) return NotFound();

			try
			{
				await _cardImageService.DeleteAsync(cardImage);
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
