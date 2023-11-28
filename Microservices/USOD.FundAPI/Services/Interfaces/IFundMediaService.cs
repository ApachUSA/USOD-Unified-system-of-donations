using Fund_Library.Entity;

namespace USOD.FundAPI.Services.Interfaces
{
	public interface IFundMediaService
	{
		Task<List<Fund_Media>> GetAsync(int fund_id);

		Task<Fund_Media?> GetByIdAsync(int fundMedia_id);

		Task<Fund_Media> CreateAsync(Fund_Media fundMedia);

		Task<List<Fund_Media>> CreateAsync(List<Fund_Media> fundMedia);

		Task<Fund_Media> UpdateAsync(Fund_Media fundMedia);

		Task<bool> DeleteAsync(Fund_Media fundMedia);
	}
}
