using Fund_Library.Entity;
using Microsoft.EntityFrameworkCore;
using USOD.FundAPI.Repositories.Interfaces;
using USOD.FundAPI.Services.Interfaces;

namespace USOD.FundAPI.Services.Implementations
{
	public class FundMemberService : IFundMemberService
	{
		private readonly IBaseRepository<Fund_Member> _fundMemberRepository;

		public FundMemberService(IBaseRepository<Fund_Member> fundMemberRepository)
		{
			_fundMemberRepository = fundMemberRepository;
		}

		public async Task<Fund_Member> CreateAsync(Fund_Member fundMember)
		{
			await _fundMemberRepository.Create(fundMember);
			return fundMember;
		}

		public async Task<bool> DeleteAsync(Fund_Member fundMember)
		{
			await _fundMemberRepository.Delete(fundMember);
			return true;
		}

		public async Task<List<Fund_Member>> GetAsync(int fund_id)
		{
			return await _fundMemberRepository.Get().Where(x => x.Fund_ID == fund_id).ToListAsync();
		}

		public async Task<List<Fund_Member>> GetByDonorAsync(int donor_id)
		{
			return await _fundMemberRepository.Get().Where(x => x.Donor_ID == donor_id).ToListAsync();
		}

		public async Task<Fund_Member?> GetByIdAsync(int fundMember_id)
		{
			return await _fundMemberRepository.Get().FirstOrDefaultAsync(x => x.Fund_ID == fundMember_id);
		}

		public async Task<Fund_Member> UpdateAsync(Fund_Member fundMember)
		{
			await _fundMemberRepository.Update(fundMember);
			return fundMember;
		}
	}
}
