using Fund_Library.Entity;
using Microsoft.EntityFrameworkCore;
using USOD.FundAPI.Repositories.Interfaces;
using USOD.FundAPI.Services.Interfaces;

namespace USOD.FundAPI.Services.Implementations
{
	public class FundMediaService : IFundMediaService
	{
		private readonly IBaseRepository<Fund_Media> _fundMediaRepository;

		public FundMediaService(IBaseRepository<Fund_Media> fundMediaRepository)
		{
			_fundMediaRepository = fundMediaRepository;
		}

		public async Task<Fund_Media> CreateAsync(Fund_Media fundMedia)
		{
			await _fundMediaRepository.Create(fundMedia);
			return fundMedia;

		}

		public async Task<List<Fund_Media>> CreateAsync(List<Fund_Media> fundMedia)
		{
			await _fundMediaRepository.Create(fundMedia);
			return fundMedia;
		}

		public async Task<bool> DeleteAsync(Fund_Media fundMedia)
		{
			await _fundMediaRepository.Delete(fundMedia);
			return true;
		}

		public async Task<List<Fund_Media>> GetAsync(int fund_id)
		{
			return await _fundMediaRepository.Get().Where(x => x.Fund_ID == fund_id).ToListAsync();
		}

		public async Task<Fund_Media?> GetByIdAsync(int fundMedia_id)
		{
			return await _fundMediaRepository.Get().FirstOrDefaultAsync(x => x.Fund_Media_ID == fundMedia_id);
		}

		public async Task<Fund_Media> UpdateAsync(Fund_Media fundImage)
		{
			await _fundMediaRepository.Update(fundImage);
			return fundImage;
		}
	}
}
