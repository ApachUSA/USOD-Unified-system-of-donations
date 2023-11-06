using Fund_Library.Entity;
using Microsoft.EntityFrameworkCore;
using USOD.FundAPI.Repositories.Interfaces;
using USOD.FundAPI.Services.Interfaces;

namespace USOD.FundAPI.Services.Implementations
{
	public class FundImageService : IFundImageService
	{
		private readonly IBaseRepository<Fund_Image> _fundImageRepository;

		public FundImageService(IBaseRepository<Fund_Image> fundImageRepository)
		{
			_fundImageRepository = fundImageRepository;
		}

		public async Task<Fund_Image> CreateAsync(Fund_Image fundImage)
		{
			await _fundImageRepository.Create(fundImage);
			return fundImage;

		}

		public async Task<bool> DeleteAsync(Fund_Image fundImage)
		{
			await _fundImageRepository.Delete(fundImage);
			return true;
		}

		public async Task<List<Fund_Image>> GetAsync(int fund_id)
		{
			return await _fundImageRepository.Get().Where(x => x.Fund_ID == fund_id).ToListAsync();
		}

		public async Task<Fund_Image?> GetByIdAsync(int fundImage_id)
		{
			return await _fundImageRepository.Get().FirstOrDefaultAsync(x => x.Fund_Image_ID == fundImage_id);
		}

		public async Task<Fund_Image> UpdateAsync(Fund_Image fundImage)
		{
			await _fundImageRepository.Update(fundImage);
			return fundImage;
		}
	}
}
