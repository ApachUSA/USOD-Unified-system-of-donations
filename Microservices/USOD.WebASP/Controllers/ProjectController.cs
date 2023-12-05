using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Project_Library.Entity;
using RealTime_Library;
using System.Xml.Linq;
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
		private readonly IReportImageService _reportImageService;
		private readonly IProjectCommentService _projectCommentService;
		private readonly IDonorService _donorService;

		public ProjectController(IProjectService projectService, IPaymentTypeService paymentTypeService, IFundService fundService, IProjectFundService projectFundService, IReportImageService reportImageService, IProjectCommentService projectCommentService, IDonorService donorService)
		{
			_projectService = projectService;
			_paymentTypeService = paymentTypeService;
			_fundService = fundService;
			_projectFundService = projectFundService;
			_reportImageService = reportImageService;
			_projectCommentService = projectCommentService;
			_donorService = donorService;
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
				if (page == "Edit")
				{
					ViewData["ProjectId"] = project_id;
					await GetFundListData();
				}

				var projectVM = new ProjectVM() { Project = response.Data };

				var fund_owner = await _fundService.GetFundByID(response.Data.Fund_ID);
				projectVM.FundOwner = fund_owner.StatusCode == System.Net.HttpStatusCode.OK ? fund_owner.Data : null;

				var fund_coops = await _projectFundService.GetByProjectIdAsync(response.Data.Project_ID);
				if (fund_coops.StatusCode == System.Net.HttpStatusCode.OK)
				{
					var funds = await _fundService.GetFundByID(fund_coops.Data.Select(x => x.Fund_ID).ToArray());
					projectVM.FundCoop = funds.StatusCode == System.Net.HttpStatusCode.OK ? funds.Data : null;
				}

				projectVM.Project_Comments = await GetProjectComments(project_id);

				return View(projectVM);
			}
			throw new Exception(response.Description);
		}

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
			var response = await _projectService.CreateProject(project);
			if (response.StatusCode == System.Net.HttpStatusCode.OK)
			{
				return RedirectToAction(nameof(ProjectIndex));
			}
			throw new Exception(response.Description);
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

		[HttpPost]
		public async Task<IActionResult> AddReportImage(Project_Report_Image reportImage, int _project_id)
		{
			var response = await _reportImageService.CreateAsync(reportImage);
			if (response.StatusCode == System.Net.HttpStatusCode.OK)
			{
				return RedirectToAction("ProjectDetails", new { project_id = _project_id, Page = "Edit" });
			}
			throw new Exception(response.Description);
		}

		[HttpGet]
		public async Task<IActionResult> DeleteReportImage(int reportImage_id, int _project_id)
		{
			var response = await _reportImageService.Delete(reportImage_id);
			if (response.StatusCode == System.Net.HttpStatusCode.OK)
			{
				return RedirectToAction("ProjectDetails", new { project_id = _project_id, Page = "Edit" });
			}
			throw new Exception(response.Description);
		}

		[HttpPost]
		public async Task<IActionResult> PostComment(Project_Comment comment)
		{
			if (int.TryParse(HttpContext.User.FindFirst("Id").Value, out int donor_id))
			{
				comment.Comment_Date = DateTime.Now;
				comment.Donor_ID = donor_id;
				var response = await _projectCommentService.CreateComment(comment);
				if (response.StatusCode == System.Net.HttpStatusCode.OK)
				{
					return Ok();
				}
				return BadRequest(response.Description);
			}
			return BadRequest("Cant find id");
		}

		[HttpPost]
		public async Task<IActionResult> GetProjectComment(Project_Comment comment)
		{
			var commentVm = new ProjectCommentVM() { Project_Comment = comment };

			var donor = await _donorService.GetInfo(comment.Donor_ID);
			commentVm.Donor = donor.StatusCode == System.Net.HttpStatusCode.OK ? donor.Data : null;

			return PartialView("_ProjectCommentPartial", commentVm);
		}

		private async Task GetFundListData()
		{
			var fund_list = await _fundService.GetList();
			if (fund_list.StatusCode == System.Net.HttpStatusCode.OK)
			{
				ViewData["FundList"] = new SelectList(fund_list.Data.OrderBy(x => x.Fund_Name), "Fund_ID", "Fund_Name");
			}
		}

		private async Task<List<ProjectCommentVM>?> GetProjectComments(int project_id)
		{
			var response = await _projectCommentService.GetList(project_id);
			if (response.StatusCode == System.Net.HttpStatusCode.OK)
			{
				var commentVms = new List<ProjectCommentVM>();
				var task = response.Data.Select(async comment =>
				{
					var commentVm = new ProjectCommentVM() { Project_Comment = comment };

					var donor = await _donorService.GetInfo(comment.Donor_ID);
					commentVm.Donor = donor.StatusCode == System.Net.HttpStatusCode.OK ? donor.Data : null;

					commentVms.Add(commentVm);
				});

				await Task.WhenAll(task);
				return commentVms.OrderByDescending(x => x.Project_Comment.Comment_Date).ToList();
			}
			return null;
		}

	}
}
