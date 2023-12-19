using Fund_Library.Entity;
using Microsoft.EntityFrameworkCore;
using USOD.FundAPI.Repositories.Interfaces;
using USOD.FundAPI.Services.Interfaces;

namespace USOD.FundAPI.Services.Implementations
{
	public class FundService : IFundService
	{
		private readonly IBaseRepository<Fund> _fundRepository;
		private readonly IFundMediaService _fundMediaService;

		public FundService(IBaseRepository<Fund> fundRepository, IFundMediaService fundMediaService)
		{
			_fundRepository = fundRepository;
			_fundMediaService = fundMediaService;
		}

		public async Task<Fund> CreateAsync(Fund fund)
		{
			await _fundRepository.Create(fund);

			List<Fund_Media> medias = new()
			{
				new Fund_Media { Fund_ID = fund.Fund_ID, Media_Type_ID = 1},
				new Fund_Media { Fund_ID = fund.Fund_ID, Media_Type_ID = 2},
				new Fund_Media { Fund_ID = fund.Fund_ID, Media_Type_ID = 3},
				new Fund_Media { Fund_ID = fund.Fund_ID, Media_Type_ID = 4}
			};

			await _fundMediaService.CreateAsync(medias);

			return fund;
		}

		public async Task<bool> DeleteAsync(Fund fund)
		{
			await _fundRepository.Delete(fund);
			return true;
		}

		public async Task<List<Fund>> GetAsync()
		{
			var funds = await _fundRepository.Get()
				.Include(x => x.Fund_Medias)
				.ThenInclude(x => x.Media_Type)
				.Include(x => x.Fund_Members)
				.ThenInclude(x => x.Member_Role)
				.ToListAsync();

			ClearAutoIncludes(funds);
			return funds;

		}

		public async Task<Fund?> GetByIdAsync(int fund_id)
		{
			return await _fundRepository.Get()
				.Include(x => x.Fund_Images)
				.Include(x => x.Fund_Medias)
				.ThenInclude(x => x.Media_Type)
				.Include(x => x.Fund_Members)
				.ThenInclude(x => x.Member_Role)
				.FirstOrDefaultAsync(x => x.Fund_ID == fund_id);
		}

		public async Task<List<Fund>> GetByIdAsync(int[] fund_ids)
		{
			var funds = await _fundRepository.Get()
				.Include(x => x.Fund_Medias)
				.ThenInclude(x => x.Media_Type)
				.Include(x => x.Fund_Members)
				.Where(x => fund_ids.Contains(x.Fund_ID))
				.ToListAsync();

			ClearAutoIncludes(funds);
			return funds;
		}

		public async Task<Fund> UpdateAsync(Fund fund)
		{
			await _fundRepository.Update(fund);
			return fund;
		}


		private void ClearAutoIncludes(List<Fund> funds)
		{
			foreach (var fund in funds)
			{
				fund.Fund_Medias?.ForEach(x => x.Media_Type.Fund_Medias = null);
				fund.Fund_Members?.ForEach(x => { if (x.Member_Role != null) x.Member_Role.Fund_Members = null; });
			}
		}
	}
}
