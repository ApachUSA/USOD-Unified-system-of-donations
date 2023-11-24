using Donor_Library.Entity;

namespace USOD.DonorAPI.Services.Interfaces
{
	public interface IDonorMediaService
	{
		Task<List<Donor_Media>> GetAsync();

		Task<Donor_Media?> GetByIDAsync(int media_id);

		Task<Donor_Media> CreateAsync(Donor_Media media);

		Task<List<Donor_Media>> CreateAsync(List<Donor_Media> media);

		Task<Donor_Media> UpdateAsync(Donor_Media media);

		Task<bool> DeleteAsync(Donor_Media media);
	}
}
