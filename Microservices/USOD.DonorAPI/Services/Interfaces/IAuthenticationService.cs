using Donor_Library.Entity;
using Donor_Library.ViewModel;

namespace USOD.DonorAPI.Services.Interfaces
{
	public interface IAuthenticationService
	{
		Task<string> AuthenticateAsync(Donor donor);
	}
}
