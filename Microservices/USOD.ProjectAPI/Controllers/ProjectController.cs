using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Project_Library.Entity;
using USOD.ProjectAPI.Services.Interfaces;

namespace USOD.ProjectAPI.Controllers
{
	[Route("ProjectApi/[controller]")]
	[ApiController]
	public class ProjectController : ControllerBase
	{
		private readonly IProjectService _projectService;
		private readonly ILogger<ProjectController> _logger;

		public ProjectController(IProjectService projectService, ILogger<ProjectController> logger)
		{
			_projectService = projectService;
			_logger = logger;
		}

		[HttpGet]
		public async Task<IActionResult> Get()
		{
			var projects = await _projectService.Get();

			if (!projects.Any()) return NotFound();

			return Ok(projects);
		}

		[HttpGet("GetByIds")]
		public async Task<IActionResult> GetByIds([FromQuery] int[] project_ids)
		{
			var projects = await _projectService.GetByIdAsync(project_ids);

			if (!projects.Any()) return NotFound();

			return Ok(projects);
		}

		[HttpGet("GetById/{project_id}")]
		public async Task<IActionResult> GetById(int project_id)
		{
			var project = await _projectService.GetByIdAsync(project_id);

			if (project == null) return NotFound();

			return Ok(project);
		}

		[HttpPost]
		public async Task<IActionResult> Post([FromBody] Project project)
		{
			try
			{
				await _projectService.CreateAsync(project);
				return Ok(project);
			}
			catch (Exception ex)
			{
				_logger.LogError("Exception: {ex}", ex.Message);
				return BadRequest(ex.Message);
			}
		}

		[HttpPut]
		public async Task<IActionResult> Put([FromBody] Project project)
		{
			try
			{
				await _projectService.UpdateAsync(project);
				return Ok(project);
			}
			catch (Exception ex)
			{
				_logger.LogError("Exception: {ex}", ex.Message);
				return BadRequest(ex.Message);
			}
		}

		[HttpDelete("{project_id}")]
		public async Task<IActionResult> Delete(int project_id)
		{
			var project = await _projectService.GetByIdAsync(project_id);
			if (project == null) return NotFound();

			try
			{
				await _projectService.DeleteAsync(project);
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
