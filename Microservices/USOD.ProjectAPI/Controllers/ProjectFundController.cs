using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Project_Library.Entity;
using USOD.ProjectAPI.Services.Interfaces;

namespace USOD.ProjectAPI.Controllers
{
	[Route("ProjectApi/[controller]")]
	[ApiController]
	public class ProjectFundController : ControllerBase
	{
		private readonly IProjectFundService _projectFundService;
		private readonly ILogger<ProjectFundController> _logger;

		public ProjectFundController(IProjectFundService projectFundService, ILogger<ProjectFundController> logger)
		{
			_projectFundService = projectFundService;
			_logger = logger;
		}


		[HttpGet]
		public async Task<IActionResult> Get()
		{
			var projectFund = await _projectFundService.GetAsync();

			if (!projectFund.Any()) return NotFound();

			return Ok(projectFund);
		}

		[HttpGet("GetById/{projectFund_id}")]
		public async Task<IActionResult> GetById(int projectFund_id)
		{
			var projectFund = await _projectFundService.GetByIdAsync(projectFund_id);

			if (projectFund == null) return NotFound();

			return Ok(projectFund);
		}

		[HttpGet("GetByFundId/{fund_id}")]
		public async Task<IActionResult> GetByFundId(int fund_id)
		{
			var projects = await _projectFundService.GetByFundIdAsync(fund_id);

			if (!projects.Any()) return NotFound();

			return Ok(projects);
		}

		[HttpGet("GetByProjectId/{project_id}")]
		public async Task<IActionResult> GetByProjectId(int project_id)
		{
			var projectFunds = await _projectFundService.GetByProjectIdAsync(project_id);

			if (!projectFunds.Any()) return NotFound();

			return Ok(projectFunds);
		}

		[HttpPost]
		public async Task<IActionResult> Post([FromBody] Project_Fund projectFund)
		{
			try
			{
				await _projectFundService.CreateAsync(projectFund);
				return Ok(projectFund);
			}
			catch (Exception ex)
			{
				_logger.LogError("Exception: {ex}", ex.Message);
				return BadRequest(ex.Message);
			}
		}

		[HttpPost("PostArray")]
		public async Task<IActionResult> PostArray([FromBody] List<Project_Fund> projectFunds)
		{
			try
			{
				await _projectFundService.CreateAsync(projectFunds);
				return Ok(projectFunds);
			}
			catch (Exception ex)
			{
				_logger.LogError("Exception: {ex}", ex.Message);
				return BadRequest(ex.Message);
			}
		}

		[HttpPut]
		public async Task<IActionResult> Put([FromBody] Project_Fund projectFund)
		{
			try
			{
				await _projectFundService.UpdateAsync(projectFund);
				return Ok(projectFund);
			}
			catch (Exception ex)
			{
				_logger.LogError("Exception: {ex}", ex.Message);
				return BadRequest(ex.Message);
			}
		}

		[HttpDelete("{projectFund_id}")]
		public async Task<IActionResult> Delete(int projectFund_id)
		{
			var projectFund = await _projectFundService.GetByIdAsync(projectFund_id);
			if (projectFund == null) return NotFound();

			try
			{
				await _projectFundService.DeleteAsync(projectFund);
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
