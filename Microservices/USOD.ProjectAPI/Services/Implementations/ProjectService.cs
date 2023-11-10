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

		public ProjectService(IBaseRepository<Project> projectRepository, IProjectPaymentService projectPaymentService, IProjectReportService projectReportService, IProjectCardService projectCardService)
		{
			_projectRepository = projectRepository;
			_projectPaymentService = projectPaymentService;
			_projectReportService = projectReportService;
			_projectCardService = projectCardService;
		}

		public async Task<Project> CreateAsync(Project project)
		{
			await _projectRepository.Create(project);
			if(project.Project_Payments != null)
			{
				project.Project_Payments.ForEach(x => x.Project_ID = project.Project_ID);
				await _projectPaymentService.CreateAsync(project.Project_Payments);
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

		public async Task<Project?> GetByIdAsync(int project_id)
		{
			return await _projectRepository.Get().FirstOrDefaultAsync(x => x.Project_ID == project_id);
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
