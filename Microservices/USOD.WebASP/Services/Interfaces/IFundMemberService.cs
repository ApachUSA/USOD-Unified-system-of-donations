using Fund_Library.Entity;
using WebASP_Library.Response;

namespace USOD.WebASP.Services.Interfaces
{
	public interface IFundMemberService
	{
		Task<BaseResponse<Fund_Member>> CreateAsync(Fund_Member fundMember);

		Task<BaseResponse<bool>> Delete(int fundMember_id);
	}
}
