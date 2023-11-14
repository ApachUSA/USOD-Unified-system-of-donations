using Microsoft.EntityFrameworkCore;
using Project_Library.Entity;
using USOD.ProjectAPI.Repositories.Interfaces;
using USOD.ProjectAPI.Services.Interfaces;

namespace USOD.ProjectAPI.Services.Implementations
{
	public class ProjectPaymentService : IProjectPaymentService
	{
		private readonly IBaseRepository<Project_Payment> _projectPaymentRepository;

		public ProjectPaymentService(IBaseRepository<Project_Payment> projectPaymentRepository)
		{
			_projectPaymentRepository = projectPaymentRepository;
		}

		public async Task<Project_Payment> CreateAsync(Project_Payment payment)
		{
			await _projectPaymentRepository.Create(payment);
			return payment;
		}

		public async Task<List<Project_Payment>> CreateAsync(List<Project_Payment> payments)
		{
			await _projectPaymentRepository.Create(payments);
			return payments;
		}

		public async Task<bool> DeleteAsync(Project_Payment payment)
		{
			await _projectPaymentRepository.Delete(payment);
			return true;
		}

		public async Task<List<Project_Payment>> GetAsync(int project_id)
		{
			return await _projectPaymentRepository.Get().Where(x => x.Project_ID == project_id).ToListAsync(); 
		}

		public async Task<Project_Payment?> GetByIdAsync(int payment_id)
		{
			return await _projectPaymentRepository.Get()
				.Include(x => x.Payment_Type)
				.FirstOrDefaultAsync(x => x.Project_Payment_ID == payment_id);
		}

		public async Task<Project_Payment> UpdateAsync(Project_Payment payment)
		{
			await _projectPaymentRepository.Update(payment);
			return payment;
		}
	}
}
