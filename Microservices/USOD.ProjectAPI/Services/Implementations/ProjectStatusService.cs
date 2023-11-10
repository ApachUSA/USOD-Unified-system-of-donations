using Microsoft.EntityFrameworkCore;
using Project_Library.Entity;
using USOD.ProjectAPI.Repositories.Interfaces;
using USOD.ProjectAPI.Services.Interfaces;

namespace USOD.ProjectAPI.Services.Implementations
{
	public class ProjectStatusService : IProjectStatusService
	{
		private readonly IBaseRepository<Project_Status> _projectStatusRepository;

		public ProjectStatusService(IBaseRepository<Project_Status> projectStatusRepository)
		{
			_projectStatusRepository = projectStatusRepository;
		}

		public async Task<Project_Status> CreateAsync(Project_Status projectStatus)
		{
			await _projectStatusRepository.Create(projectStatus);
			return projectStatus;
		}

		public async Task<bool> DeleteAsync(Project_Status projectStatus)
		{
			await _projectStatusRepository.Delete(projectStatus);
			return true;
		}

		public async Task<List<Project_Status>> GetAsync()
		{
			return await _projectStatusRepository.Get().ToListAsync();
		}

		public async Task<Project_Status?> GetByIdAsync(int projectStatus_id)
		{
			return await _projectStatusRepository.Get().FirstOrDefaultAsync(x => x.Project_Status_ID == projectStatus_id);
		}

		public async Task<Project_Status> UpdateAsync(Project_Status projectStatus)
		{
			await _projectStatusRepository.Update(projectStatus);
			return projectStatus;
		}
	}
}
