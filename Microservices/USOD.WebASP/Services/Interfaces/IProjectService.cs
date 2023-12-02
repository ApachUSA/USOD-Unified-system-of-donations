using Project_Library.Entity;
using WebASP_Library.Response;

namespace USOD.WebASP.Services.Interfaces
{
	public interface IProjectService
	{
		Task<BaseResponse<List<Project>>> GetList();

		Task<BaseResponse<Project>> GetPojectByID(int project_id);

		Task<BaseResponse<List<Project>>> GetPojectByID(int[] project_ids);

		Task<BaseResponse<List<Project>>> GetPojectByFund(int fund_id);

		Task<BaseResponse<Project>> CreateProject(Project project);

		Task<BaseResponse<Project>> UpdateProject(Project project);

		Task<BaseResponse<bool>> DeleteProject(int project_id);
	}
}
