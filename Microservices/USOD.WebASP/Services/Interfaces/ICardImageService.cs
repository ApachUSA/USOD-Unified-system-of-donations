using Fund_Library.Entity;
using Project_Library.Entity;
using WebASP_Library.Response;

namespace USOD.WebASP.Services.Interfaces
{
	public interface ICardImageService
	{
		Task<BaseResponse<Card_Image>> CreateAsync(Card_Image cardImage);

		Task<BaseResponse<bool>> Delete(int cardImage_id);
	}
}
