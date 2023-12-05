using Project_Library.Entity;
using RealTime_Library;
using WebASP_Library.Response;

namespace USOD.WebASP.Services.Interfaces
{
	public interface IProjectCommentService
	{
		Task<BaseResponse<List<Project_Comment>>> GetList(int project_id);

		Task<BaseResponse<bool>> CreateComment(Project_Comment comment);
	}
}
