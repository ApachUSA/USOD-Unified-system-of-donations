﻿using Microsoft.EntityFrameworkCore;
using Project_Library.Entity;
using USOD.ProjectAPI.Repositories.Interfaces;
using USOD.ProjectAPI.Services.Interfaces;

namespace USOD.ProjectAPI.Services.Implementations
{
	public class ProjectFundService : IProjectFundService
	{
		private readonly IBaseRepository<Project_Fund> _projectFundRepository;
		private readonly IProjectService _projectService;

		public ProjectFundService(IBaseRepository<Project_Fund> projectFundRepository, IProjectService projectService)
		{
			_projectFundRepository = projectFundRepository;
			_projectService = projectService;
		}

		public async Task<Project_Fund> CreateAsync(Project_Fund projectFund)
		{
			await _projectFundRepository.Create(projectFund);
			return projectFund;
		}

		public async Task<List<Project_Fund>> CreateAsync(List<Project_Fund> projectFunds)
		{
			await _projectFundRepository.Create(projectFunds);
			return projectFunds;
		}

		public async Task<bool> DeleteAsync(Project_Fund projectFund)
		{
			await _projectFundRepository.Delete(projectFund);
			return true;
		}

		public async Task<List<Project_Fund>> GetAsync()
		{
			return await _projectFundRepository.Get().ToListAsync();
		}

		public async Task<List<Project>> GetByFundIdAsync(int fund_id)
		{
			var project_ids = await _projectFundRepository.Get().Where(x => x.Fund_ID == fund_id).Select(x => x.Project_ID).ToArrayAsync();
			return await _projectService.GetByIdAsync(project_ids);
		}

		public async Task<Project_Fund?> GetByIdAsync(int projectFund_id)
		{
			return await _projectFundRepository.Get().FirstOrDefaultAsync(x => x.Project_Fund_ID == projectFund_id);
		}

		public async Task<List<Project_Fund>> GetByProjectIdAsync(int project_id)
		{
			return await _projectFundRepository.Get().Where(x => x.Project_ID == project_id).ToListAsync();
		}

		public async Task<Project_Fund> UpdateAsync(Project_Fund projectFund)
		{
			await _projectFundRepository.Update(projectFund);
			return projectFund;
		}
	}
}
