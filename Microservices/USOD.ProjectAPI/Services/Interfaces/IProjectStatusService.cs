using Project_Library.Entity;

namespace USOD.ProjectAPI.Services.Interfaces
{
	public interface IProjectStatusService
	{
		Task<List<Project_Status>> GetAsync();

		Task<Project_Status?> GetByIdAsync(int projectStatus_id);

		Task<Project_Status> CreateAsync(Project_Status projectStatus);

		Task<Project_Status> UpdateAsync(Project_Status projectStatus);

		Task<bool> DeleteAsync(Project_Status projectStatus);
	}
}
