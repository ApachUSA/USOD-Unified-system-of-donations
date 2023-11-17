using Microsoft.EntityFrameworkCore;
using RealTime_Library;
using USOD.RealTimeAPI.Repositories.Interfaces;
using USOD.RealTimeAPI.Services.Interfaces;

namespace USOD.RealTimeAPI.Services.Implementations
{
	public class ProjectCommentService : IProjectCommentService
	{
		private readonly IBaseRepository<Project_Comment> _projectCommentRepository;

		public ProjectCommentService(IBaseRepository<Project_Comment> projectCommentRepository)
		{
			_projectCommentRepository = projectCommentRepository;
		}

		public async Task<Project_Comment> CreateAsync(Project_Comment comment)
		{
			await _projectCommentRepository.Create(comment);
			return comment;
		}

		public async Task<bool> DeleteAsync(Project_Comment comment)
		{
			await _projectCommentRepository.Delete(comment);
			return true;
		}

		public async Task<List<Project_Comment>> GetAsync(int project_id)
		{
			return await _projectCommentRepository.Get().Where(x => x.Project_ID == project_id).ToListAsync();
		}

		public async Task<Project_Comment?> GetByIdAsync(int comment_id)
		{
			return await _projectCommentRepository.Get().FirstOrDefaultAsync(x => x.Comment_ID == comment_id);
		}
	}
}
