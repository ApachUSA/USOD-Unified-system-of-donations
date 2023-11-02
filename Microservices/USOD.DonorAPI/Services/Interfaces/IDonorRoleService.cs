using Donor_Library.Entity;
using Donor_Library.ViewModel;

namespace USOD.DonorAPI.Services.Interfaces
{
	public interface IDonorRoleService
	{
		Task<List<Donor_Role>> GetAsync();

		Task<Donor_Role?> GetByIDAsync(int donor_id);

		Task<Donor_Role> CreateAsync(Donor_Role donor);

		Task<Donor_Role> UpdateAsync(Donor_Role donor);

		Task<bool> DeleteAsync(Donor_Role donor);
	}
}
