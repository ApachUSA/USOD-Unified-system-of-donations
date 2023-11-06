using Fund_Library.Entity;
using Microsoft.EntityFrameworkCore;
using USOD.FundAPI.Repositories.Interfaces;
using USOD.FundAPI.Services.Interfaces;

namespace USOD.FundAPI.Services.Implementations
{
	public class FundService : IFundService
	{
		private readonly IBaseRepository<Fund> _fundRepository;

		public FundService(IBaseRepository<Fund> fundRepository)
		{
			_fundRepository = fundRepository;
		}

		public async Task<Fund> CreateAsync(Fund fund)
		{
			await _fundRepository.Create(fund);
			return fund;
		}

		public async Task<bool> DeleteAsync(Fund fund)
		{
			await _fundRepository.Delete(fund);
			return true;
		}

		public async Task<List<Fund>> GetAsync()
		{
			return await _fundRepository.Get().ToListAsync();
		}

		public async Task<Fund?> GetByIdAsync(int fund_id)
		{
			return await _fundRepository.Get().FirstOrDefaultAsync(x => x.Fund_ID == fund_id);
		}

		public async Task<Fund> UpdateAsync(Fund fund)
		{
			await _fundRepository.Update(fund);
			return fund;
		}
	}
}
