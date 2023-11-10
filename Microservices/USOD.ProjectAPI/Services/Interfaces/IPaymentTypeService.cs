using Project_Library.Entity;

namespace USOD.ProjectAPI.Services.Interfaces
{
	public interface IPaymentTypeService
	{
		Task<List<Payment_Type>> GetAsync();

		Task<Payment_Type?> GetByIdAsync(int paymentType_id);

		Task<Payment_Type> CreateAsync(Payment_Type paymentType);

		Task<Payment_Type> UpdateAsync(Payment_Type paymentType);

		Task<bool> DeleteAsync(Payment_Type paymentType);
	}
}
