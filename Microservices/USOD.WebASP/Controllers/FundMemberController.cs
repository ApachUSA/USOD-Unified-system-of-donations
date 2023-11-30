using Fund_Library.Entity;
using Microsoft.AspNetCore.Mvc;
using USOD.WebASP.Services.Interfaces;

namespace USOD.WebASP.Controllers
{
	public class FundMemberController : Controller
	{
		private readonly IFundMemberService _fundMemberService;
		private readonly IFundMemberRoleService _fundMemberRoleService;

		public FundMemberController(IFundMemberService fundMemberService, IFundMemberRoleService fundMemberRoleService)
		{
			_fundMemberService = fundMemberService;
			_fundMemberRoleService = fundMemberRoleService;
		}

		[HttpPost]
		public async Task<IActionResult> AddMember(Fund_Member fund_Member)
		{
			var response_role = await _fundMemberRoleService.GetList();
			if(response_role.StatusCode == System.Net.HttpStatusCode.OK)
			{
				fund_Member.Member_Role_ID = response_role.Data.Where(x => x.Member_Role_Name == "Member").Select(x => x.Member_Role_ID).FirstOrDefault();

				var response = await _fundMemberService.CreateAsync(fund_Member);
				if (response.StatusCode == System.Net.HttpStatusCode.OK)
				{
					return RedirectToAction("FundDetails", "Fund", new { fund_id = fund_Member.Fund_ID, edit = true });
				}
				throw new Exception(response.Description);
			}
			throw new Exception(response_role.Description);
		}

		[HttpGet]
		public async Task<IActionResult> DeleteMember(int fund_Member_id, int _fund_id)
		{
			var response = await _fundMemberService.Delete(fund_Member_id);
			if (response.StatusCode == System.Net.HttpStatusCode.OK)
			{
				return RedirectToAction("FundDetails", "Fund", new { fund_id = _fund_id, edit = true });
			}
			throw new Exception(response.Description);
		}
	}
}
