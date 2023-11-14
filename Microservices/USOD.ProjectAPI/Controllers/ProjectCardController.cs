using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Project_Library.Entity;
using USOD.ProjectAPI.Services.Interfaces;

namespace USOD.ProjectAPI.Controllers
{
	[Route("ProjectApi/[controller]")]
	[ApiController]
	public class ProjectCardController : ControllerBase
	{
		private readonly IProjectCardService _projectCardService;
		private readonly ILogger<ProjectCardController> _logger;

		public ProjectCardController(IProjectCardService projectCardService, ILogger<ProjectCardController> logger)
		{
			_projectCardService = projectCardService;
			_logger = logger;
		}


		[HttpGet("{project_id}")]
		public async Task<IActionResult> Get(int project_id)
		{
			var cards = await _projectCardService.GetAsync(project_id);

			if (!cards.Any()) return NotFound();

			return Ok(cards);
		}

		[HttpGet("GetById/{card_id}")]
		public async Task<IActionResult> GetById(int card_id)
		{
			var card = await _projectCardService.GetByIdAsync(card_id);

			if (card == null) return NotFound();

			return Ok(card);
		}

		[HttpPost]
		public async Task<IActionResult> Post([FromBody] Project_Card card)
		{
			try
			{
				await _projectCardService.CreateAsync(card);
				return Ok(card);
			}
			catch (Exception ex)
			{
				_logger.LogError("Exception: {ex}", ex.Message);
				return BadRequest(ex.Message);
			}
		}

		[HttpPost("PostArray")]
		public async Task<IActionResult> PostArray([FromBody] List<Project_Card> cards)
		{
			try
			{
				await _projectCardService.CreateAsync(cards);
				return Ok(cards);
			}
			catch (Exception ex)
			{
				_logger.LogError("Exception: {ex}", ex.Message);
				return BadRequest(ex.Message);
			}
		}

		[HttpPut]
		public async Task<IActionResult> Put([FromBody] Project_Card card)
		{
			try
			{
				await _projectCardService.UpdateAsync(card);
				return Ok(card);
			}
			catch (Exception ex)
			{
				_logger.LogError("Exception: {ex}", ex.Message);
				return BadRequest(ex.Message);
			}
		}

		[HttpDelete("{card_id}")]
		public async Task<IActionResult> Delete(int card_id)
		{
			var card = await _projectCardService.GetByIdAsync(card_id);
			if (card == null) return NotFound();

			try
			{
				await _projectCardService.DeleteAsync(card);
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
