using Fund_Library.Entity;

namespace USOD.FundAPI.Services.Interfaces
{
	public interface IFundImageService
	{
		Task<List<Fund_Image>> GetAsync(int fund_id);

		Task<Fund_Image?> GetByIdAsync(int fundImage_id);

		Task<Fund_Image> CreateAsync(Fund_Image fundImage);

		Task<Fund_Image> UpdateAsync(Fund_Image fundImage);

		Task<bool> DeleteAsync(Fund_Image fundImage);
	}
}
