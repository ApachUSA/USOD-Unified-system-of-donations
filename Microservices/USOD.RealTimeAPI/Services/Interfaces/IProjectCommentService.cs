using RealTime_Library;

namespace USOD.RealTimeAPI.Services.Interfaces
{
	public interface IProjectCommentService
	{
		Task<List<Project_Comment>> GetAsync(int project_id);

		Task<Project_Comment?> GetByIdAsync(int comment_id);

		Task<Project_Comment> CreateAsync(Project_Comment comment);

		Task<bool> DeleteAsync(Project_Comment comment);
	}
}
