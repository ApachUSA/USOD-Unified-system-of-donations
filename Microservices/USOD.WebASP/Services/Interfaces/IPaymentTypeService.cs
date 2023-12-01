using Project_Library.Entity;
using WebASP_Library.Response;

namespace USOD.WebASP.Services.Interfaces
{
	public interface IPaymentTypeService
	{
		Task<BaseResponse<List<Payment_Type>>> GetList();
	}
}
