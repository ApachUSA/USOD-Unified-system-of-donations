using Project_Library.Entity;

namespace USOD.ProjectAPI.Services.Interfaces
{
	public interface IProjectPaymentService
	{
		Task<List<Project_Payment>> GetAsync(int project_id);

		Task<Project_Payment?> GetByIdAsync(int payment_id);

		Task<Project_Payment> CreateAsync(Project_Payment payment);

		Task<List<Project_Payment>> CreateAsync(List<Project_Payment> payments);

		Task<Project_Payment> UpdateAsync(Project_Payment payment);

		Task<bool> DeleteAsync(Project_Payment payment);
	}
}
