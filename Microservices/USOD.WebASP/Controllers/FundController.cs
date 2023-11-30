using Fund_Library.Entity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using USOD.WebASP.Services.Implementations;
using USOD.WebASP.Services.Interfaces;
using WebASP_Library.ViewModel;

namespace USOD.WebASP.Controllers
{
	public class FundController : Controller
	{
		private readonly IFundService _fundService;
		private readonly IDonorService _donorService;
		private readonly IFundMemberRoleService _memberRoleService;

		public FundController(IFundService fundService, IDonorService donorService, IFundMemberRoleService memberRoleService)
		{
			_fundService = fundService;
			_donorService = donorService;
			_memberRoleService = memberRoleService;
		}

		public async Task<IActionResult> FundIndex()
		{
			var response = await _fundService.GetList();
			if (response.StatusCode == System.Net.HttpStatusCode.OK)
			{
				List<FundVM> fundVM = new();
				var tasks = response.Data.Select(async fund =>
				{
					var donors = await _donorService.GetInfo(fund.Fund_Members.Select(x => x.Donor_ID).ToArray());
					fundVM.Add(new FundVM
					{
						Fund = fund,
						Fund_Members = donors.StatusCode == System.Net.HttpStatusCode.OK ? donors.Data : null
					});
				});

				await Task.WhenAll(tasks);
				return View(fundVM);
			}
			throw new Exception(response.Description);
		}

		[HttpGet]
		public async Task<IActionResult> FundDetails(int fund_id, bool edit = false)
		{
			var response = await _fundService.GetFundByID(fund_id);
			if (response.StatusCode == System.Net.HttpStatusCode.OK)
			{				
				ViewData["Edit"] = edit;
				if (edit)
				{
					await GetDonorListData();		
				}
				var donors = await _donorService.GetInfo(response.Data.Fund_Members.Select(x => x.Donor_ID).ToArray());
				return View(new FundVM
				{
					Fund = response.Data,
					Fund_Members = donors.StatusCode == System.Net.HttpStatusCode.OK ? donors.Data : null
				});
			}
			throw new Exception(response.Description);
		}

		[HttpGet]
		public async Task<IActionResult> FundEdit(int fund_id)
		{
			var response = await _fundService.GetFundByID(fund_id);
			if (response.StatusCode == System.Net.HttpStatusCode.OK)
			{
				return View(response.Data);
			}
			throw new Exception(response.Description);
		}

		[HttpPost]
		public async Task<IActionResult> FundEdit(Fund fund)
		{
			var response = await _fundService.UpdateFund(fund);
			if (response.StatusCode == System.Net.HttpStatusCode.OK)
			{
				return RedirectToAction("FundDetails", new { fund_id = fund.Fund_ID });
			}
			throw new Exception(response.Description);
		}

		[HttpGet]
		public async Task<IActionResult> FundCreate() 
		{
			await GetDonorListData();
			return View();
		}

		[HttpPost]
		public async Task<IActionResult> FundCreate(Fund fund)
		{
			var response_role = await _memberRoleService.GetList();
			if (response_role.StatusCode == System.Net.HttpStatusCode.OK)
			{
				fund.Fund_Members[0].Member_Role_ID = response_role.Data.Where(x => x.Member_Role_Name == "Owner").Select(x => x.Member_Role_ID).FirstOrDefault();

				var response = await _fundService.CreateFund(fund);
				if (response.StatusCode == System.Net.HttpStatusCode.OK)
				{
					//user list
					return RedirectToAction(nameof(FundIndex));
				}
				throw new Exception(response.Description);
			}
			throw new Exception(response_role.Description);
		}

		public async Task<IActionResult> FundDelete(int fund_id)
		{
			var response = await _fundService.DeleteFund(fund_id);
			if (response.StatusCode == System.Net.HttpStatusCode.OK)
			{
				return RedirectToAction(nameof(FundIndex));
			}
			throw new Exception(response.Description);
		}

		private async Task GetDonorListData()
		{
			var donor_list = await _donorService.GetList();
			if (donor_list.StatusCode == System.Net.HttpStatusCode.OK)
			{
				ViewData["DonorList"] = new SelectList(donor_list.Data.OrderBy(x => x.Username), "Donor_ID", "Username");
			}
		}

	}
}
