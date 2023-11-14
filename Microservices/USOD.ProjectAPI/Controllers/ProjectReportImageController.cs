using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Project_Library.Entity;
using USOD.ProjectAPI.Services.Interfaces;

namespace USOD.ProjectAPI.Controllers
{
	[Route("ProjectApi/[controller]")]
	[ApiController]
	public class ReportImageController : ControllerBase
	{
		private readonly IProjectReportImageService _reportImageService;
		private readonly ILogger<ReportImageController> _logger;

		public ReportImageController(IProjectReportImageService reportImageService, ILogger<ReportImageController> logger)
		{
			_reportImageService = reportImageService;
			_logger = logger;
		}


		[HttpGet("{report_id}")]
		public async Task<IActionResult> Get(int report_id)
		{
			var reportImages = await _reportImageService.GetAsync(report_id);

			if (!reportImages.Any()) return NotFound();

			return Ok(reportImages);
		}

		[HttpGet("GetById/{reportImage_id}")]
		public async Task<IActionResult> GetById(int reportImage_id)
		{
			var reportImage = await _reportImageService.GetByIdAsync(reportImage_id);

			if (reportImage == null) return NotFound();

			return Ok(reportImage);
		}

		[HttpPost]
		public async Task<IActionResult> Post([FromBody] Project_Report_Image reportImage)
		{
			try
			{
				await _reportImageService.CreateAsync(reportImage);
				return Ok(reportImage);
			}
			catch (Exception ex)
			{
				_logger.LogError("Exception: {ex}", ex.Message);
				return BadRequest(ex.Message);
			}
		}

		[HttpPost("PostArray")]
		public async Task<IActionResult> PostArray([FromBody] List<Project_Report_Image> reportImages)
		{
			try
			{
				await _reportImageService.CreateAsync(reportImages);
				return Ok(reportImages);
			}
			catch (Exception ex)
			{
				_logger.LogError("Exception: {ex}", ex.Message);
				return BadRequest(ex.Message);
			}
		}

		[HttpPut]
		public async Task<IActionResult> Put([FromBody] Project_Report_Image reportImage)
		{
			try
			{
				await _reportImageService.UpdateAsync(reportImage);
				return Ok(reportImage);
			}
			catch (Exception ex)
			{
				_logger.LogError("Exception: {ex}", ex.Message);
				return BadRequest(ex.Message);
			}
		}

		[HttpDelete("{reportImage_id}")]
		public async Task<IActionResult> Delete(int reportImage_id)
		{
			var reportImage = await _reportImageService.GetByIdAsync(reportImage_id);
			if (reportImage == null) return NotFound();

			try
			{
				await _reportImageService.DeleteAsync(reportImage);
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
