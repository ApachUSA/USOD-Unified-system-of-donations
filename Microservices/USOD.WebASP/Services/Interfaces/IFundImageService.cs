using Fund_Library.Entity;
using WebASP_Library.Response;

namespace USOD.WebASP.Services.Interfaces
{
	public interface IFundImageService
	{
		Task<BaseResponse<List<Fund_Image>>> GetList(int fund_id);

		Task<BaseResponse<Fund_Image>> CreateAsync(Fund_Image fundImage);

		Task<BaseResponse<bool>> Delete(int fundImage_id);
	}
}
