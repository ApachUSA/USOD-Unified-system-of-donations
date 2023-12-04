using Project_Library.Entity;
using WebASP_Library.Response;

namespace USOD.WebASP.Services.Interfaces
{
	public interface ICardVideoService
	{
		Task<BaseResponse<Card_Video>> CreateAsync(Card_Video cardVideo);

		Task<BaseResponse<bool>> Delete(int cardVideo_id);
	}
}
