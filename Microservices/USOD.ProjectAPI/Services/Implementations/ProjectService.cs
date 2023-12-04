using Microsoft.EntityFrameworkCore;
using Project_Library.Entity;
using USOD.ProjectAPI.Repositories.Interfaces;
using USOD.ProjectAPI.Services.Interfaces;

namespace USOD.ProjectAPI.Services.Implementations
{
	public class ProjectService : IProjectService
	{
		private readonly IBaseRepository<Project> _projectRepository;
		private readonly IProjectReportService _projectReportService;

		public ProjectService(IBaseRepository<Project> projectRepository, IProjectReportService projectReportService)
		{
			_projectRepository = projectRepository;
			_projectReportService = projectReportService;
		}

		public async Task<Project> CreateAsync(Project project)
		{
			await _projectRepository.Create(project);
			//if(project.Project_Payments != null)
			//{
			//	project.Project_Payments.ForEach(x => x.Project_ID = project.Project_ID);
			//	await _projectPaymentService.CreateAsync(project.Project_Payments);
			//}

			//if (project.Project_Funds != null)
			//{
			//	project.Project_Funds.ForEach(x => x.Project_ID = project.Project_ID);
			//	await _projectFundService.CreateAsync(project.Project_Funds);
			//}

			await _projectReportService.CreateAsync(new Project_Report() { Project_ID = project.Project_ID });

			//if (project.Project_Cards != null)
			//{
			//	project.Project_Cards.ForEach(x => x.Project_ID = project.Project_ID);
			//	await _projectCardService.CreateAsync(project.Project_Cards);
			//}

			return project;
		}

		public async Task<bool> DeleteAsync(Project project)
		{
			await _projectRepository.Delete(project);
			return true;
		}

		public async Task<List<Project>> Get()
		{
			return await _projectRepository.Get()
				.Include(x => x.Project_Cards)
				.ThenInclude(x => x.Item_Tag)
				.Include(x => x.Project_Status)
				.ToListAsync();
		}

		public async Task<List<Project>> GetByFundAsync(int fund_id)
		{
			return await _projectRepository.Get()
				.Include(x => x.Project_Cards)
				.ThenInclude(x => x.Item_Tag)
				.Include(x => x.Project_Status)
				.Where(x => x.Fund_ID == fund_id)
				.ToListAsync();
		}

		public async Task<Project?> GetByIdAsync(int project_id)
		{
			return await _projectRepository.Get()
				.Include(x => x.Project_Cards)
				.ThenInclude(x => x.Card_Images)
				.Include(x => x.Project_Cards)
				.ThenInclude(x => x.Card_Videos)
				.Include(x => x.Project_Cards)
				.ThenInclude(x => x.Item_Tag)
				.Include(x => x.Project_Status)
				.Include(x => x.Project_Payments)
				.ThenInclude(x => x.Payment_Type)
				.Include(x => x.Project_Report)
				.ThenInclude(x => x.Images)
				.FirstOrDefaultAsync(x => x.Project_ID == project_id);
		}

		public async Task<List<Project>> GetByIdAsync(int[] project_ids)
		{
			return await _projectRepository.Get()
				.Include(x => x.Project_Cards)
				.ThenInclude(x => x.Item_Tag)
				.Include(x => x.Project_Status)
				.Where(x => project_ids.Contains(x.Project_ID))
				.ToListAsync();
		}

		public async Task<Project> UpdateAsync(Project project)
		{
			await _projectRepository.Update(project);
			return project;
		}
	}
}
