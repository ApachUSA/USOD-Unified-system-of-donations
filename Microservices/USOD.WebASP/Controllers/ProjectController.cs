using Microsoft.AspNetCore.Mvc;
using Project_Library.Entity;
using USOD.WebASP.Services.Interfaces;

namespace USOD.WebASP.Controllers
{
	public class ProjectController : Controller
	{
		private readonly IProjectService _projectService;
		private readonly IPaymentTypeService _paymentTypeService;

		public ProjectController(IProjectService projectService, IPaymentTypeService paymentTypeService)
		{
			_projectService = projectService;
			_paymentTypeService = paymentTypeService;
		}

		public async Task<IActionResult> ProjectIndex()
		{
			var response = await _projectService.GetList();
			if (response.StatusCode == System.Net.HttpStatusCode.OK)
			{
				return View(response.Data);
			}
			throw new Exception(response.Description);
		}

		[HttpGet]
		public async Task<IActionResult> ProjectDetails(int project_id, bool edit = false)
		{
			var response = await _projectService.GetPojectByID(project_id);
			if (response.StatusCode == System.Net.HttpStatusCode.OK)
			{
				ViewData["Edit"] = edit;
				return View(response.Data);
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
		public async Task<IActionResult> ProjectCreate()
		{
			//await GetDonorListData();
			var response = await _paymentTypeService.GetList();
			if(response.StatusCode == System.Net.HttpStatusCode.OK)
			{
				ViewData["PaymentType"] = response.Data;
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

		//private async Task GetDonorListData()
		//{
		//	var donor_list = await _donorService.GetList();
		//	if (donor_list.StatusCode == System.Net.HttpStatusCode.OK)
		//	{
		//		ViewData["DonorList"] = new SelectList(donor_list.Data.OrderBy(x => x.Username), "Donor_ID", "Username");
		//	}
		//}
	}
}
