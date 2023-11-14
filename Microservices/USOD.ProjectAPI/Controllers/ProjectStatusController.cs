using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Project_Library.Entity;
using USOD.ProjectAPI.Services.Interfaces;

namespace USOD.ProjectAPI.Controllers
{
	[Route("ProjectApi/[controller]")]
	[ApiController]
	public class ProjectStatusController : ControllerBase
	{
		private readonly IProjectStatusService _projectStatusService;
		private readonly ILogger<ProjectStatusController> _logger;

		public ProjectStatusController(IProjectStatusService projectStatusService, ILogger<ProjectStatusController> logger)
		{
			_projectStatusService = projectStatusService;
			_logger = logger;
		}


		[HttpGet]
		public async Task<IActionResult> Get()
		{
			var projectStatus = await _projectStatusService.GetAsync();

			if (!projectStatus.Any()) return NotFound();

			return Ok(projectStatus);
		}

		[HttpGet("GetById/{projectStatus_id}")]
		public async Task<IActionResult> GetById(int projectStatus_id)
		{
			var projectStatus = await _projectStatusService.GetByIdAsync(projectStatus_id);

			if (projectStatus == null) return NotFound();

			return Ok(projectStatus);
		}

		[HttpPost]
		public async Task<IActionResult> Post([FromBody] Project_Status projectStatus)
		{
			try
			{
				await _projectStatusService.CreateAsync(projectStatus);
				return Ok(projectStatus);
			}
			catch (Exception ex)
			{
				_logger.LogError("Exception: {ex}", ex.Message);
				return BadRequest(ex.Message);
			}
		}

		[HttpPut]
		public async Task<IActionResult> Put([FromBody] Project_Status projectStatus)
		{
			try
			{
				await _projectStatusService.UpdateAsync(projectStatus);
				return Ok(projectStatus);
			}
			catch (Exception ex)
			{
				_logger.LogError("Exception: {ex}", ex.Message);
				return BadRequest(ex.Message);
			}
		}

		[HttpDelete("{projectStatus_id}")]
		public async Task<IActionResult> Delete(int projectStatus_id)
		{
			var projectStatus = await _projectStatusService.GetByIdAsync(projectStatus_id);
			if (projectStatus == null) return NotFound();

			try
			{
				await _projectStatusService.DeleteAsync(projectStatus);
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
