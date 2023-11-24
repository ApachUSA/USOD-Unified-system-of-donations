using Donor_Library.Entity;

namespace USOD.DonorAPI.Services.Interfaces
{
	public interface IDonorMediaTypeService
	{
		Task<List<Donor_Media_Type>> GetAsync();

		Task<Donor_Media_Type?> GetByIDAsync(int mediaType_id);

		Task<Donor_Media_Type> CreateAsync(Donor_Media_Type mediaType);

		Task<Donor_Media_Type> UpdateAsync(Donor_Media_Type mediaType);

		Task<bool> DeleteAsync(Donor_Media_Type mediaType);
	}
}
