using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Project_Library.Entity;
using USOD.WebASP.Services.Interfaces;
using WebASP_Library.ViewModel;

namespace USOD.WebASP.Controllers
{
	public class ProjectController : Controller
	{
		private readonly IProjectService _projectService;
		private readonly IFundService _fundService;
		private readonly IPaymentTypeService _paymentTypeService;
		private readonly IProjectFundService _projectFundService;

		public ProjectController(IProjectService projectService, IPaymentTypeService paymentTypeService, IFundService fundService, IProjectFundService projectFundService)
		{
			_projectService = projectService;
			_paymentTypeService = paymentTypeService;
			_fundService = fundService;
			_projectFundService = projectFundService;
		}

		public async Task<IActionResult> ProjectIndex()
		{
			var response = await _projectService.GetList();
			if (response.StatusCode == System.Net.HttpStatusCode.OK)
			{

				var projectTasks = response.Data.Select(async project =>
				{
					var projectVM = new ProjectVM() { Project = project };

					var fund_owner = await _fundService.GetFundByID(project.Fund_ID);
					projectVM.FundOwner = fund_owner.StatusCode == System.Net.HttpStatusCode.OK ? fund_owner.Data : null;

					var fund_coops = await _projectFundService.GetByProjectIdAsync(project.Project_ID);
					if (fund_coops.StatusCode == System.Net.HttpStatusCode.OK)
					{
						var funds = await _fundService.GetFundByID(fund_coops.Data.Select(x => x.Fund_ID).ToArray());
						projectVM.FundCoop = funds.StatusCode == System.Net.HttpStatusCode.OK ? funds.Data : null;
					}

					return projectVM;
				});

				var projects = await Task.WhenAll(projectTasks);
				ViewData["Index"] = true;
				return View(projects.ToList());
			}
			throw new Exception(response.Description);
		}


		[HttpGet]
		public async Task<IActionResult> ProjectDetails(int project_id, string page = "Index")
		{
			var response = await _projectService.GetPojectByID(project_id);
			if (response.StatusCode == System.Net.HttpStatusCode.OK)
			{
				ViewData["Page"] = page;
				if (page == "Edit") await GetFundListData();

				var projectVM = new ProjectVM() { Project = response.Data };

				var fund_owner = await _fundService.GetFundByID(response.Data.Fund_ID);
				projectVM.FundOwner = fund_owner.StatusCode == System.Net.HttpStatusCode.OK ? fund_owner.Data : null;

				var fund_coops = await _projectFundService.GetByProjectIdAsync(response.Data.Project_ID);
				if (fund_coops.StatusCode == System.Net.HttpStatusCode.OK)
				{
					var funds = await _fundService.GetFundByID(fund_coops.Data.Select(x => x.Fund_ID).ToArray());
					projectVM.FundCoop = funds.StatusCode == System.Net.HttpStatusCode.OK ? funds.Data : null;
				}

				return View(projectVM);
			}
			throw new Exception(response.Description);
		}

		//[HttpGet]
		//public async Task<IActionResult> projectEdit(int project_id)
		//{
		//	var response = await _projectService.GetprojectByID(project_id);
		//	if (response.StatusCode == System.Net.HttpStatusCode.OK)
		//	{
		//		return View(response.Data);
		//	}
		//	throw new Exception(response.Description);
		//}

		[HttpPost]
		public async Task<IActionResult> ProjectEdit(Project project)
		{
			var response = await _projectService.UpdateProject(project);
			if (response.StatusCode == System.Net.HttpStatusCode.OK)
			{
				return RedirectToAction("ProjectDetails", new { project_id = project.Project_ID });
			}
			throw new Exception(response.Description);
		}

		[HttpGet]
		public async Task<IActionResult> ProjectCreate(int fund_id)
		{
			//await GetDonorListData();
			var response = await _paymentTypeService.GetList();
			if (response.StatusCode == System.Net.HttpStatusCode.OK)
			{
				ViewData["PaymentType"] = response.Data;
				ViewData["FundId"] = fund_id;
				ViewData["Page"] = "Create";
				return View();
			}
			throw new Exception(response.Description);
		}

		[HttpPost]
		public async Task<IActionResult> ProjectCreate(Project project)
		{
			//STATUS mb
			//var response_role = await _memberRoleService.GetList();
			//if (response_role.StatusCode == System.Net.HttpStatusCode.OK)
			//{
			//	project.project_Members[0].Member_Role_ID = response_role.Data.Where(x => x.Member_Role_Name == "Owner").Select(x => x.Member_Role_ID).FirstOrDefault();

			var response = await _projectService.CreateProject(project);
			if (response.StatusCode == System.Net.HttpStatusCode.OK)
			{
				//user list
				return RedirectToAction(nameof(ProjectIndex));
			}
			throw new Exception(response.Description);
			//}
			//throw new Exception(response_role.Description);
		}

		public async Task<IActionResult> ProjectDelete(int project_id)
		{
			var response = await _projectService.DeleteProject(project_id);
			if (response.StatusCode == System.Net.HttpStatusCode.OK)
			{
				return RedirectToAction(nameof(ProjectIndex));
			}
			throw new Exception(response.Description);
		}

		private async Task GetFundListData()
		{
			var fund_list = await _fundService.GetList();
			if (fund_list.StatusCode == System.Net.HttpStatusCode.OK)
			{
				ViewData["FundList"] = new SelectList(fund_list.Data.OrderBy(x => x.Fund_Name), "Fund_ID", "Fund_Name");
			}
		}

	}
}
