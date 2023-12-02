using Fund_Library.Entity;
using Microsoft.AspNetCore.Mvc;
using Project_Library.Entity;
using USOD.WebASP.Services.Interfaces;

namespace USOD.WebASP.Controllers
{
	public class ProjectFundController : Controller
	{
		private readonly IProjectFundService _projectFundService;

		public ProjectFundController(IProjectFundService projectFundService)
		{
			_projectFundService = projectFundService;
		}

		[HttpPost]
		public async Task<IActionResult> AddCoop(Project_Fund fund_Member)
		{
			var response = await _projectFundService.CreateProjectFund(fund_Member);
			if (response.StatusCode == System.Net.HttpStatusCode.OK)
			{
				return RedirectToAction("ProjectDetails", "Project", new { project_id = fund_Member.Project_ID, page = "Edit" });
			}
			throw new Exception(response.Description);

		}

		[HttpGet]
		public async Task<IActionResult> DeleteCoop(int fund_id, int project_id)
		{
			var coop_list = await _projectFundService.GetByProjectIdAsync(project_id);
			if(coop_list.StatusCode == System.Net.HttpStatusCode.OK)
			{
				var projectFund = coop_list.Data.Where(x => x.Fund_ID == fund_id).FirstOrDefault();

				if(projectFund != null)
				{
					var response = await _projectFundService.DeleteProjectFund(projectFund.Project_Fund_ID);
					if (response.StatusCode == System.Net.HttpStatusCode.OK)
					{
						return RedirectToAction("ProjectDetails", "Project", new { project_id = project_id, page = "Edit" });
					}
					throw new Exception(response.Description);
				}
				
			}			
			throw new Exception(coop_list.Description);
		}
	}
}

