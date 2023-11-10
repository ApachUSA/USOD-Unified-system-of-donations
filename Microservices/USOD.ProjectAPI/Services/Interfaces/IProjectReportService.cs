using Project_Library.Entity;

namespace USOD.ProjectAPI.Services.Interfaces
{
	public interface IProjectReportService
	{
		Task<Project_Report?> GetAsync(int project_id);

		Task<Project_Report?> GetByIdAsync(int report_id);

		Task<Project_Report> CreateAsync(Project_Report report);

		Task<Project_Report> UpdateAsync(Project_Report report);

		Task<bool> DeleteAsync(Project_Report report);
	}
}
