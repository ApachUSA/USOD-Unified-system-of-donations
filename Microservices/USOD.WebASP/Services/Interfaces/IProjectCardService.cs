using Project_Library.Entity;
using WebASP_Library.Response;

namespace USOD.WebASP.Services.Interfaces
{
	public interface IProjectCardService
	{
		Task<BaseResponse<Project_Card>> GetByIdAsync(int projectCard_id);

		Task<BaseResponse<Project_Card>> CreateProjectCard(Project_Card projectCard);

		Task<BaseResponse<Project_Card>> UpdateProjectCard(Project_Card projectCard);

		Task<BaseResponse<bool>> DeleteProjectCard(int projectCard_id);
	}
}
