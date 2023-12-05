using Fund_Library.Entity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Project_Library.Entity;
using RealTime_Library;
using System;
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
		private readonly IProjectService _projectService;
		private readonly IProjectFundService _projectFundService;
		private readonly IFundSubscriptionService _fundSubscriptionService;

		public FundController(IFundService fundService, IDonorService donorService, IFundMemberRoleService memberRoleService, IProjectService projectService, IProjectFundService projectFundService, IFundSubscriptionService fundSubscriptionService)
		{
			_fundService = fundService;
			_donorService = donorService;
			_memberRoleService = memberRoleService;
			_projectService = projectService;
			_projectFundService = projectFundService;
			_fundSubscriptionService = fundSubscriptionService;
		}

		public async Task<IActionResult> FundIndex()
		{
			var response = await _fundService.GetList();
			if (response.StatusCode == System.Net.HttpStatusCode.OK)
			{
				List<FundVM> fundVM = new();
				var tasks = response.Data.Select(async fund =>
				{
					var vm = new FundVM() { Fund = fund };

					var donors = await _donorService.GetInfo(fund.Fund_Members.Select(x => x.Donor_ID).ToArray());
					vm.Fund_Members = donors.StatusCode == System.Net.HttpStatusCode.OK ? donors.Data : null;

					var projects = await _projectService.GetPojectByFund(fund.Fund_ID);
					vm.ProjectCount = projects.StatusCode == System.Net.HttpStatusCode.OK ? projects.Data.Count : 0;

					var subs = await _fundSubscriptionService.GetList(fund.Fund_ID);
					vm.SubscribersCount = subs.StatusCode == System.Net.HttpStatusCode.OK ? subs.Data.Count : 0;

					fundVM.Add(vm);
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
				if (edit) await GetDonorListData();

				var donors = await _donorService.GetInfo(response.Data.Fund_Members.Select(x => x.Donor_ID).ToArray());


				FundVM fundVM = new()
				{
					Fund = response.Data,
					Fund_Members = donors.StatusCode == System.Net.HttpStatusCode.OK ? donors.Data : null,
				};

				var subs = await _fundSubscriptionService.GetList(fund_id);
				fundVM.SubscribersCount = subs.StatusCode == System.Net.HttpStatusCode.OK ? subs.Data.Count : 0;

				if (int.TryParse(HttpContext.User.FindFirst("Id").Value, out int donor_id))
				{
					var sub = await _fundSubscriptionService.GetByDonor(new Subscription() { Fund_ID = fund_id, Donor_ID = donor_id });
					fundVM.Subscription = sub.StatusCode == System.Net.HttpStatusCode.OK ? sub.Data : null;
				}

				var projects = await _projectService.GetPojectByFund(fund_id);
				if (projects.StatusCode == System.Net.HttpStatusCode.OK)
				{

					var tasks = projects.Data.Select(async project =>
					{
						var projectVM = new ProjectVM() { Project = project };

						var fund_coops = await _projectFundService.GetByProjectIdAsync(project.Project_ID);
						if (fund_coops.StatusCode == System.Net.HttpStatusCode.OK)
						{
							var funds = await _fundService.GetFundByID(fund_coops.Data.Select(x => x.Fund_ID).ToArray());
							projectVM.FundCoop = funds.StatusCode == System.Net.HttpStatusCode.OK ? funds.Data : null;
						}

						return projectVM;
					});
					fundVM.Projects = (await Task.WhenAll(tasks)).ToList();

				}
				ViewData["Index"] = false;
				return View(fundVM);
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

		[HttpPost]
		public async Task<IActionResult> FundSubscribe(int fund_id)
		{
			if (int.TryParse(HttpContext.User.FindFirst("Id").Value, out int donor_id))
			{
				var response = await _fundSubscriptionService.Subscribe(new Subscription() { Fund_ID = fund_id, Donor_ID = donor_id });
				if (response.StatusCode == System.Net.HttpStatusCode.OK)
				{
					return Ok(fund_id);
				}
				return BadRequest(response.Description);
			}
			return BadRequest("Cant find id");
		}

		[HttpPost]
		public async Task<IActionResult> FundUnSubscribe(int sub_id)
		{
			var response = await _fundSubscriptionService.Unsubscribe(sub_id);
			if (response.StatusCode == System.Net.HttpStatusCode.OK)
			{
				return Ok();
			}
			return BadRequest(response.Description);
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
