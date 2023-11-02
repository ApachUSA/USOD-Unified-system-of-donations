using Donor_Library.Entity;
using Donor_Library.ViewModel;
using Microsoft.AspNetCore.Mvc;

namespace USOD.DonorAPI.Services.Interfaces
{
	public interface IDonorService
	{
		Task<List<Donor>> GetAsync();

		Task<Donor?> GetByIDAsync(int donor_role_id);

		Task<Donor?> GetByLoginAsync(Donor_LoginVM donor_Login);

		Task<Donor> RegisterAsync(Donor donor);

		Task<Donor> UpdateAsync(Donor donor);

		Task<bool> DeleteAsync(Donor donor);
	}
}
