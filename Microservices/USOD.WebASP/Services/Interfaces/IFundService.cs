using Donor_Library.Entity;
using Fund_Library.Entity;
using WebASP_Library.Response;

namespace USOD.WebASP.Services.Interfaces
{
	public interface IFundService
	{
		Task<BaseResponse<List<Fund>>> GetList();

		Task<BaseResponse<Fund>> GetFundByID(int fund_id);

		Task<BaseResponse<Fund>> CreateFund(Fund fund);

		Task<BaseResponse<Fund>> UpdateFund(Fund fund);

		Task<BaseResponse<bool>> DeleteFund(int fund_id);

	}
}
