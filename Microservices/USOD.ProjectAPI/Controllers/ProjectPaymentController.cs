using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Project_Library.Entity;
using USOD.ProjectAPI.Services.Interfaces;

namespace USOD.ProjectAPI.Controllers
{
	[Route("ProjectApi/[controller]")]
	[ApiController]
	public class ProjectPaymentController : ControllerBase
	{
		private readonly IProjectPaymentService _projectPaymentService;
		private readonly ILogger<ProjectPaymentController> _logger;

		public ProjectPaymentController(IProjectPaymentService projectPaymentService, ILogger<ProjectPaymentController> logger)
		{
			_projectPaymentService = projectPaymentService;
			_logger = logger;
		}


		[HttpGet("{project_id}")]
		public async Task<IActionResult> Get(int project_id)
		{
			var projectPayments = await _projectPaymentService.GetAsync(project_id);

			if (!projectPayments.Any()) return NotFound();

			return Ok(projectPayments);
		}

		[HttpGet("GetById/{projectPayment_id}")]
		public async Task<IActionResult> GetById(int projectPayment_id)
		{
			var projectPayment = await _projectPaymentService.GetByIdAsync(projectPayment_id);

			if (projectPayment == null) return NotFound();

			return Ok(projectPayment);
		}

		[HttpPost]
		public async Task<IActionResult> Post([FromBody] Project_Payment projectPayment)
		{
			try
			{
				await _projectPaymentService.CreateAsync(projectPayment);
				return Ok(projectPayment);
			}
			catch (Exception ex)
			{
				_logger.LogError("Exception: {ex}", ex.Message);
				return BadRequest(ex.Message);
			}
		}

		[HttpPost("PostArray")]
		public async Task<IActionResult> PostArray([FromBody] List<Project_Payment> projectPayments)
		{
			try
			{
				await _projectPaymentService.CreateAsync(projectPayments);
				return Ok(projectPayments);
			}
			catch (Exception ex)
			{
				_logger.LogError("Exception: {ex}", ex.Message);
				return BadRequest(ex.Message);
			}
		}

		[HttpPut]
		public async Task<IActionResult> Put([FromBody] Project_Payment projectPayment)
		{
			try
			{
				await _projectPaymentService.UpdateAsync(projectPayment);
				return Ok(projectPayment);
			}
			catch (Exception ex)
			{
				_logger.LogError("Exception: {ex}", ex.Message);
				return BadRequest(ex.Message);
			}
		}

		[HttpDelete("{projectPayment_id}")]
		public async Task<IActionResult> Delete(int projectPayment_id)
		{
			var projectPayment = await _projectPaymentService.GetByIdAsync(projectPayment_id);
			if (projectPayment == null) return NotFound();

			try
			{
				await _projectPaymentService.DeleteAsync(projectPayment);
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
