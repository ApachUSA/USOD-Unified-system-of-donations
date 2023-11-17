using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using RealTime_Library;
using USOD.RealTimeAPI.Hubs;
using USOD.RealTimeAPI.Services.Interfaces;

namespace USOD.RealTimeAPI.Controllers
{
	[Route("RealTimeApi/[controller]")]
	[ApiController]
	public class ProjectCommentController : ControllerBase
	{
		private readonly IProjectCommentService _projectCommentService;
		private readonly IHubContext<CommentHub, ICommentClient> _hubContext;
		private readonly ILogger<ProjectCommentController> _logger;

		public ProjectCommentController(IProjectCommentService projectCommentService, IHubContext<CommentHub, ICommentClient> hubContext, ILogger<ProjectCommentController> logger)
		{
			_projectCommentService = projectCommentService;
			_hubContext = hubContext;
			_logger = logger;
		}

		[HttpGet("{project_id}")]
		public async Task<IActionResult> Get(int project_id)
		{
			var projectComment = await _projectCommentService.GetAsync(project_id);

			if (!projectComment.Any()) return NotFound();

			return Ok(projectComment);
		}

		[HttpGet("GetById/{projectComment_id}")]
		public async Task<IActionResult> GetById(int projectComment_id)
		{
			var projectComment = await _projectCommentService.GetByIdAsync(projectComment_id);

			if (projectComment == null) return NotFound();

			return Ok(projectComment);
		}

		[HttpPost]
		public async Task<IActionResult> Post([FromBody] Project_Comment projectComment)
		{
			try
			{
				await _projectCommentService.CreateAsync(projectComment);

				await _hubContext.Clients.Group(projectComment.Project_ID.ToString()).ReceiveComment(projectComment);

				return Ok();
			}
			catch (Exception ex)
			{
				_logger.LogError("Exception: {ex}", ex.Message);
				return BadRequest(ex.Message);
			}
		}

		//[HttpPut]
		//public async Task<IActionResult> Put([FromBody] Card_Image projectComment)
		//{
		//	try
		//	{
		//		await _projectCommentService.UpdateAsync(projectComment);
		//		return Ok(projectComment);
		//	}
		//	catch (Exception ex)
		//	{
		//		_logger.LogError("Exception: {ex}", ex.Message);
		//		return BadRequest(ex.Message);
		//	}
		//}

		[HttpDelete("{projectComment_id}")]
		public async Task<IActionResult> Delete(int projectComment_id)
		{
			var projectComment = await _projectCommentService.GetByIdAsync(projectComment_id);
			if (projectComment == null) return NotFound();

			try
			{
				await _projectCommentService.DeleteAsync(projectComment);
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
