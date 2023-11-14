using Microsoft.EntityFrameworkCore;
using Project_Library.Entity;
using USOD.ProjectAPI.Repositories.Interfaces;
using USOD.ProjectAPI.Services.Interfaces;

namespace USOD.ProjectAPI.Services.Implementations
{
	public class ProjectReportService : IProjectReportService
	{
		private readonly IBaseRepository<Project_Report> _projectReportRepository;

		public ProjectReportService(IBaseRepository<Project_Report> projectReportRepository)
		{
			_projectReportRepository = projectReportRepository;
		}

		public async Task<Project_Report> CreateAsync(Project_Report report)
		{
			await _projectReportRepository.Create(report);
			return report;
		}

		public async Task<bool> DeleteAsync(Project_Report report)
		{
			await _projectReportRepository.Delete(report);
			return true;
		}

		public async Task<Project_Report?> GetAsync(int project_id)
		{
			return await _projectReportRepository.Get()
				.Include(x => x.Images)
				.FirstOrDefaultAsync(x => x.Project_ID == project_id);
		}

		public async Task<Project_Report?> GetByIdAsync(int report_id)
		{
			return await _projectReportRepository.Get()
				.Include(x => x.Images)
				.FirstOrDefaultAsync(x => x.Project_Report_ID == report_id);
		}

		public async Task<Project_Report> UpdateAsync(Project_Report report)
		{
			await _projectReportRepository.Update(report);
			return report;
		}
	}
}
