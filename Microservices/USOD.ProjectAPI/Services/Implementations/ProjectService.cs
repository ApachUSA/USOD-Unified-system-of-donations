using Microsoft.EntityFrameworkCore;
using Project_Library.Entity;
using USOD.ProjectAPI.Repositories.Interfaces;
using USOD.ProjectAPI.Services.Interfaces;

namespace USOD.ProjectAPI.Services.Implementations
{
	public class ProjectService : IProjectService
	{
		private readonly IBaseRepository<Project> _projectRepository;
		private readonly IProjectPaymentService _projectPaymentService;
		private readonly IProjectReportService _projectReportService;
		private readonly IProjectCardService _projectCardService;
		private readonly IProjectFundService _projectFundService;

		public ProjectService(IBaseRepository<Project> projectRepository, IProjectPaymentService projectPaymentService, IProjectReportService projectReportService, IProjectCardService projectCardService, IProjectFundService projectFundService)
		{
			_projectRepository = projectRepository;
			_projectPaymentService = projectPaymentService;
			_projectReportService = projectReportService;
			_projectCardService = projectCardService;
			_projectFundService = projectFundService;
		}

		public async Task<Project> CreateAsync(Project project)
		{
			await _projectRepository.Create(project);
			if(project.Project_Payments != null)
			{
				project.Project_Payments.ForEach(x => x.Project_ID = project.Project_ID);
				await _projectPaymentService.CreateAsync(project.Project_Payments);
			}

			if (project.Project_Funds != null)
			{
				project.Project_Funds.ForEach(x => x.Project_ID = project.Project_ID);
				await _projectFundService.CreateAsync(project.Project_Funds);
			}

			await _projectReportService.CreateAsync(new Project_Report() { Project_ID = project.Project_ID });

			if (project.Project_Cards != null)
			{
				project.Project_Cards.ForEach(x => x.Project_ID = project.Project_ID);
				await _projectCardService.CreateAsync(project.Project_Cards);
			}

			return project;
		}

		public async Task<bool> DeleteAsync(Project project)
		{
			await _projectRepository.Delete(project);
			return true;
		}

		public async Task<List<Project>> Get()
		{
			return await _projectRepository.Get().ToListAsync();
		}

		public async Task<Project?> GetByIdAsync(int project_id)
		{
			return await _projectRepository.Get()
				.Include(x => x.Project_Cards)
				.Include(x => x.Project_Status)
				.Include(x => x.Project_Payments)
				.Include(x => x.Project_Report)
				.FirstOrDefaultAsync(x => x.Project_ID == project_id);
		}

		public async Task<List<Project>> GetByIdAsync(int[] project_ids)
		{
			return await _projectRepository.Get().Where(x => project_ids.Contains(x.Project_ID)).ToListAsync();
		}

		public async Task<Project> UpdateAsync(Project project)
		{
			await _projectRepository.Update(project);
			return project;
		}
	}
}
