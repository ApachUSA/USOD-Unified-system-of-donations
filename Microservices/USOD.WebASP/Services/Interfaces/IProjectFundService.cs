using Project_Library.Entity;
using WebASP_Library.Response;

namespace USOD.WebASP.Services.Interfaces
{
	public interface IProjectFundService
	{
		Task<BaseResponse<List<Project_Fund>>> GetByFundIdAsync(int fund_id);

		Task<BaseResponse<List<Project_Fund>>> GetByProjectIdAsync(int project_id);

		Task<BaseResponse<Project_Fund>> CreateProjectFund(Project_Fund projectFund);

		Task<BaseResponse<bool>> DeleteProjectFund(int projectFund_id);
	}
}
