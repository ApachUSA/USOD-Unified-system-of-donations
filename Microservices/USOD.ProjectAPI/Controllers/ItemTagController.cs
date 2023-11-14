using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Project_Library.Entity;
using USOD.ProjectAPI.Services.Interfaces;

namespace USOD.ProjectAPI.Controllers
{
	[Route("ProjectApi/[controller]")]
	[ApiController]
	public class ItemTagController : ControllerBase
	{
		private readonly IItemTagService _itemTagService;
		private readonly ILogger<ItemTagController> _logger;

		public ItemTagController(IItemTagService itemTagService, ILogger<ItemTagController> logger)
		{
			_itemTagService = itemTagService;
			_logger = logger;
		}


		[HttpGet]
		public async Task<IActionResult> Get()
		{
			var itemTags = await _itemTagService.GetAsync();

			if (!itemTags.Any()) return NotFound();

			return Ok(itemTags);
		}

		[HttpGet("GetById/{itemTag_id}")]
		public async Task<IActionResult> GetById(int itemTag_id)
		{
			var itemTag = await _itemTagService.GetByIdAsync(itemTag_id);

			if (itemTag == null) return NotFound();

			return Ok(itemTag);
		}

		[HttpPost]
		public async Task<IActionResult> Post([FromBody] Item_Tag itemTag)
		{
			try
			{
				await _itemTagService.CreateAsync(itemTag);
				return Ok(itemTag);
			}
			catch (Exception ex)
			{
				_logger.LogError("Exception: {ex}", ex.Message);
				return BadRequest(ex.Message);
			}
		}

		[HttpPut]
		public async Task<IActionResult> Put([FromBody] Item_Tag itemTag)
		{
			try
			{
				await _itemTagService.UpdateAsync(itemTag);
				return Ok(itemTag);
			}
			catch (Exception ex)
			{
				_logger.LogError("Exception: {ex}", ex.Message);
				return BadRequest(ex.Message);
			}
		}

		[HttpDelete("{itemTag_id}")]
		public async Task<IActionResult> Delete(int itemTag_id)
		{
			var itemTag = await _itemTagService.GetByIdAsync(itemTag_id);
			if (itemTag == null) return NotFound();

			try
			{
				await _itemTagService.DeleteAsync(itemTag);
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
