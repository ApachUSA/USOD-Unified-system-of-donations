using Fund_Library.Entity;

namespace USOD.FundAPI.Services.Interfaces
{
	public interface IFundMemberService
	{
		Task<List<Fund_Member>> GetAsync(int fund_id);

		Task<Fund_Member?> GetByIdAsync(int fundMember_id);

		Task<List<Fund_Member>> GetByDonorAsync(int donor_id);

		Task<Fund_Member> CreateAsync(Fund_Member fundMember);

		Task<Fund_Member> UpdateAsync(Fund_Member fundMember);

		Task<bool> DeleteAsync(Fund_Member fundMember);
	}
}
