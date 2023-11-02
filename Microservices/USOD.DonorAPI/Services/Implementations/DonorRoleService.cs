using Donor_Library.Entity;
using Microsoft.EntityFrameworkCore;
using USOD.DonorAPI.Repositories.Interfaces;
using USOD.DonorAPI.Services.Interfaces;

namespace USOD.DonorAPI.Services.Implementations
{
	public class DonorRoleService : IDonorRoleService
	{
		private readonly IBaseRepository<Donor_Role> _donor_roleRepository;

		public DonorRoleService(IBaseRepository<Donor_Role> donor_roleRepository)
		{
			_donor_roleRepository = donor_roleRepository;
		}

		public async Task<Donor_Role> CreateAsync(Donor_Role donor)
		{
			await _donor_roleRepository.Create(donor);
			return donor;
		}

		public async Task<bool> DeleteAsync(Donor_Role donor)
		{
			await _donor_roleRepository.Delete(donor);
			return true;
		}

		public async Task<List<Donor_Role>> GetAsync()
		{
			return await _donor_roleRepository.Get().ToListAsync();
		}

		public async Task<Donor_Role?> GetByIDAsync(int donor_role_id)
		{
			return await _donor_roleRepository.Get().FirstOrDefaultAsync(x => x.Donor_Role_ID == donor_role_id);
		}

		public async Task<Donor_Role> UpdateAsync(Donor_Role donor)
		{
			return await _donor_roleRepository.Update(donor);
		}
	}
}
