using Project_Library.Entity;

namespace USOD.ProjectAPI.Services.Interfaces
{
	public interface IProjectReportImageService
	{
		Task<List<Project_Report_Image>> GetAsync(int report_id);

		Task<Project_Report_Image?> GetByIdAsync(int reportImage_id);

		Task<Project_Report_Image> CreateAsync(Project_Report_Image reportImage);

		Task<List<Project_Report_Image>> CreateAsync(List<Project_Report_Image> reportImages);

		Task<Project_Report_Image> UpdateAsync(Project_Report_Image reportImage);

		Task<bool> DeleteAsync(Project_Report_Image reportImage);
	}
}
