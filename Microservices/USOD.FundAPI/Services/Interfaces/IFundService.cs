using Fund_Library.Entity;

namespace USOD.FundAPI.Services.Interfaces
{
	public interface IFundService
	{
		Task<List<Fund>> GetAsync();

		Task<Fund?> GetByIdAsync(int fund_id);

		Task<List<Fund>> GetByIdAsync(int[] fund_ids);

		Task<Fund> CreateAsync(Fund fund);

		Task<Fund> UpdateAsync(Fund fund);

		Task<bool> DeleteAsync(Fund fund);
	}
}
