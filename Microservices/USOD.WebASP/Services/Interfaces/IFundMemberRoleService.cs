using Fund_Library.Entity;
using WebASP_Library.Response;

namespace USOD.WebASP.Services.Interfaces
{
	public interface IFundMemberRoleService
	{
		Task<BaseResponse<List<Member_Role>>> GetList();
	}
}
