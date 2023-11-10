using Project_Library.Entity;

namespace USOD.ProjectAPI.Services.Interfaces
{
	public interface IProjectCardService
	{
		Task<List<Project_Card>> GetAsync(int project_id);

		Task<Project_Card?> GetByIdAsync(int card_id);

		Task<Project_Card> CreateAsync(Project_Card card);

		Task<List<Project_Card>> CreateAsync(List<Project_Card> cards);

		Task<Project_Card> UpdateAsync(Project_Card card);

		Task<bool> DeleteAsync(Project_Card card);
	}
}
