﻿using Project_Library.Entity;

namespace USOD.ProjectAPI.Services.Interfaces
{
	public interface IProjectService
	{
		Task<Project?> GetByIdAsync(int project_id);

		Task<List<Project>> GetByIdAsync(int[] project_ids);

		Task<Project> CreateAsync(Project project);

		Task<Project> UpdateAsync(Project project);

		Task<bool> DeleteAsync(Project project);
	}
}
