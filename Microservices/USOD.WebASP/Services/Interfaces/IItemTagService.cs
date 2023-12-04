using Project_Library.Entity;
using WebASP_Library.Response;

namespace USOD.WebASP.Services.Interfaces
{
	public interface IItemTagService
	{
		Task<BaseResponse<List<Item_Tag>>> GetList();
	}
}
