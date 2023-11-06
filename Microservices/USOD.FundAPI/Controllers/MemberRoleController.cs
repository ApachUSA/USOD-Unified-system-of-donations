using Fund_Library.Entity;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using USOD.FundAPI.Services.Interfaces;

namespace USOD.FundAPI.Controllers
{
	[Route("FundApi/[controller]")]
	[ApiController]
	public class MemberRoleController : ControllerBase
	{
		private readonly IMemberRoleService  _memberRoleService;
		private readonly ILogger<MemberRoleController> _logger;

		public MemberRoleController(IMemberRoleService memberRoleService, ILogger<MemberRoleController> logger)
		{
			_memberRoleService = memberRoleService;
			_logger = logger;
		}

		[HttpGet]
		public async Task<IActionResult> Get()
		{
			var memberRole = await _memberRoleService.GetAsync();

			if (!memberRole.Any()) return NotFound();

			return Ok(memberRole);
		}

		[HttpGet("{id}")]
		public async Task<IActionResult> Get(int id)
		{
			var memberRole = await _memberRoleService.GetByIdAsync(id);

			if (memberRole == null) return NotFound();

			return Ok(memberRole);
		}

		[HttpPost]
		public async Task<IActionResult> Post([FromBody] Member_Role memberRole)
		{
			try
			{
				await _memberRoleService.CreateAsync(memberRole);
				return Ok(memberRole);
			}
			catch (Exception ex)
			{
				_logger.LogError("Exception: {ex}", ex.Message);
				return BadRequest(ex.Message);
			}
		}

		[HttpPut]
		public async Task<IActionResult> Put([FromBody] Member_Role memberRole)
		{
			try
			{
				await _memberRoleService.UpdateAsync(memberRole);
				return Ok(memberRole);
			}
			catch (Exception ex)
			{
				_logger.LogError("Exception: {ex}", ex.Message);
				return BadRequest(ex.Message);
			}
		}

		[HttpDelete("{id}")]
		public async Task<IActionResult> Delete(int id)
		{
			var memberRole = await _memberRoleService.GetByIdAsync(id);
			if (memberRole == null) return NotFound();

			try
			{
				await _memberRoleService.DeleteAsync(memberRole);
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
