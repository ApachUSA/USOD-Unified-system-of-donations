using RealTime_Library;
using WebASP_Library.Response;

namespace USOD.WebASP.Services.Interfaces
{
	public interface IFundSubscriptionService
	{
		Task<BaseResponse<List<Subscription>>> GetList(int fund_id);

		Task<BaseResponse<Subscription>> GetByDonor(Subscription sub);

		Task<BaseResponse<List<Subscription>>> GetListByDonor(int donor_id);

		Task<BaseResponse<bool>> Subscribe(Subscription sub);

		Task<BaseResponse<bool>> Unsubscribe(int sub_id);
	}
}
