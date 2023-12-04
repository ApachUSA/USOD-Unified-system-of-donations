using Project_Library.Entity;
using WebASP_Library.Response;

namespace USOD.WebASP.Services.Interfaces
{
	public interface IReportImageService
	{
		Task<BaseResponse<Project_Report_Image>> CreateAsync(Project_Report_Image reportImage);

		Task<BaseResponse<bool>> Delete(int reportImage_id);
	}
}
