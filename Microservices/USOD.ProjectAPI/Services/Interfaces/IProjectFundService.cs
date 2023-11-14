using Project_Library.Entity;

namespace USOD.ProjectAPI.Services.Interfaces
{
	public interface IProjectFundService
	{
		Task<List<Project_Fund>> GetAsync();

		Task<Project_Fund?> GetByIdAsync(int projectFund_id);

		Task<List<Project>> GetByFundIdAsync(int fund_id);

		Task<List<Project_Fund>> GetByProjectIdAsync(int project_id);

		Task<Project_Fund> CreateAsync(Project_Fund projectFund);

		Task<List<Project_Fund>> CreateAsync(List<Project_Fund> projectFunds);

		Task<Project_Fund> UpdateAsync(Project_Fund projectFund);

		Task<bool> DeleteAsync(Project_Fund projectFund);
	}
}
