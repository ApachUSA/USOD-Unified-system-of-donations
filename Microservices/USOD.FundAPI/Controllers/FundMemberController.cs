using Fund_Library.Entity;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using USOD.FundAPI.Services.Interfaces;

namespace USOD.FundAPI.Controllers
{
	[Route("FundApi/[controller]")]
	[ApiController]
	public class FundMemberController : ControllerBase
	{
		private readonly IFundMemberService _fundMemberService;
		private readonly ILogger<FundMemberController> _logger;

		public FundMemberController(IFundMemberService fundMemberService, ILogger<FundMemberController> logger)
		{
			_fundMemberService = fundMemberService;
			_logger = logger;
		}

		[HttpGet("{fund_Id}")]
		public async Task<IActionResult> Get(int fund_Id)
		{
			var fundMember = await _fundMemberService.GetAsync(fund_Id);

			if (!fundMember.Any()) return NotFound();

			return Ok(fundMember);
		}

		[HttpGet("GetById/{id}")]
		public async Task<IActionResult> GetById(int id)
		{
			var fundMember = await _fundMemberService.GetByIdAsync(id);

			if (fundMember == null) return NotFound();

			return Ok(fundMember);
		}

		[HttpGet("GetByDonor/{donor_id}")]
		public async Task<IActionResult> GetByDonor(int donor_id)
		{
			var fundMember = await _fundMemberService.GetByDonorAsync(donor_id);

			if (!fundMember.Any()) return NotFound();

			if (!fundMember.Any(x => x.Member_Role_ID == 1)) return Ok("Owner");
			else return Ok("Member");

		}

		[HttpPost]
		public async Task<IActionResult> Post([FromBody] Fund_Member fundMember)
		{
			try
			{
				await _fundMemberService.CreateAsync(fundMember);
				return Ok(fundMember);
			}
			catch (Exception ex)
			{
				_logger.LogError("Exception: {ex}", ex.Message);
				return BadRequest(ex.Message);
			}
		}

		[HttpPut]
		public async Task<IActionResult> Put([FromBody] Fund_Member fundMember)
		{
			try
			{
				await _fundMemberService.UpdateAsync(fundMember);
				return Ok(fundMember);
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
			var fundMember = await _fundMemberService.GetByIdAsync(id);
			if (fundMember == null) return NotFound();

			try
			{
				await _fundMemberService.DeleteAsync(fundMember);
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
