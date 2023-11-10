using Microsoft.EntityFrameworkCore;
using Project_Library.Entity;
using USOD.ProjectAPI.Repositories.Interfaces;
using USOD.ProjectAPI.Services.Interfaces;

namespace USOD.ProjectAPI.Services.Implementations
{
	public class ProjectReportImageService : IProjectReportImageService
	{
		private readonly IBaseRepository<Project_Report_Image> _reportImageRepository;

		public ProjectReportImageService(IBaseRepository<Project_Report_Image> reportImageRepository)
		{
			_reportImageRepository = reportImageRepository;
		}

		public async Task<Project_Report_Image> CreateAsync(Project_Report_Image reportImage)
		{
			await _reportImageRepository.Create(reportImage);
			return reportImage;
		}

		public async Task<List<Project_Report_Image>> CreateAsync(List<Project_Report_Image> reportImages)
		{
			await _reportImageRepository.Create(reportImages);
			return reportImages;
		}

		public async Task<bool> DeleteAsync(Project_Report_Image reportImage)
		{
			await _reportImageRepository.Delete(reportImage);
			return true;
		}

		public async Task<List<Project_Report_Image>> GetAsync(int report_id)
		{
			return await _reportImageRepository.Get().Where(x => x.Project_Report_ID == report_id).ToListAsync();
		}

		public async Task<Project_Report_Image?> GetByIdAsync(int reportImage_id)
		{
			return await _reportImageRepository.Get().FirstOrDefaultAsync(x => x.Project_Report_Image_ID == reportImage_id);
		}

		public async Task<Project_Report_Image> UpdateAsync(Project_Report_Image reportImage)
		{
			await _reportImageRepository.Update(reportImage);
			return reportImage;
		}
	}
}
