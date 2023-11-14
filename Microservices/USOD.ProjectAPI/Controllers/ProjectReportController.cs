using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Project_Library.Entity;
using USOD.ProjectAPI.Services.Interfaces;

namespace USOD.ProjectAPI.Controllers
{
	[Route("ProjectApi/[controller]")]
	[ApiController]
	public class ProjectReportController : ControllerBase
	{
		private readonly IProjectReportService _projectReportService;
		private readonly ILogger<ProjectReportController> _logger;

		public ProjectReportController(IProjectReportService projectReportService, ILogger<ProjectReportController> logger)
		{
			_projectReportService = projectReportService;
			_logger = logger;
		}


		[HttpGet("{project_id}")]
		public async Task<IActionResult> Get(int project_id)
		{
			var projectReport = await _projectReportService.GetAsync(project_id);

			if (projectReport == null) return NotFound();

			return Ok(projectReport);
		}

		[HttpGet("GetById/{projectReport_id}")]
		public async Task<IActionResult> GetById(int projectReport_id)
		{
			var projectReport = await _projectReportService.GetByIdAsync(projectReport_id);

			if (projectReport == null) return NotFound();

			return Ok(projectReport);
		}

		[HttpPost]
		public async Task<IActionResult> Post([FromBody] Project_Report projectReport)
		{
			try
			{
				await _projectReportService.CreateAsync(projectReport);
				return Ok(projectReport);
			}
			catch (Exception ex)
			{
				_logger.LogError("Exception: {ex}", ex.Message);
				return BadRequest(ex.Message);
			}
		}

		[HttpPut]
		public async Task<IActionResult> Put([FromBody] Project_Report projectReport)
		{
			try
			{
				await _projectReportService.UpdateAsync(projectReport);
				return Ok(projectReport);
			}
			catch (Exception ex)
			{
				_logger.LogError("Exception: {ex}", ex.Message);
				return BadRequest(ex.Message);
			}
		}

		[HttpDelete("{projectReport_id}")]
		public async Task<IActionResult> Delete(int projectReport_id)
		{
			var projectReport = await _projectReportService.GetByIdAsync(projectReport_id);
			if (projectReport == null) return NotFound();

			try
			{
				await _projectReportService.DeleteAsync(projectReport);
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
