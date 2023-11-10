using Microsoft.EntityFrameworkCore;
using Project_Library.Entity;
using USOD.ProjectAPI.Repositories.Interfaces;
using USOD.ProjectAPI.Services.Interfaces;

namespace USOD.ProjectAPI.Services.Implementations
{
	public class PaymentTypeService : IPaymentTypeService
	{
		private readonly IBaseRepository<Payment_Type> _paymentTypeRepository;

		public PaymentTypeService(IBaseRepository<Payment_Type> paymentTypeRepository)
		{
			_paymentTypeRepository = paymentTypeRepository;
		}

		public async Task<Payment_Type> CreateAsync(Payment_Type paymentType)
		{
			await _paymentTypeRepository.Create(paymentType);
			return paymentType;
		}

		public async Task<bool> DeleteAsync(Payment_Type paymentType)
		{
			await _paymentTypeRepository.Delete(paymentType);
			return true;
		}

		public async Task<List<Payment_Type>> GetAsync()
		{
			return await _paymentTypeRepository.Get().ToListAsync();
		}

		public async Task<Payment_Type?> GetByIdAsync(int paymentType_id)
		{
			return await _paymentTypeRepository.Get().FirstOrDefaultAsync(x => x.Payment_Type_ID == paymentType_id);
		}

		public async Task<Payment_Type> UpdateAsync(Payment_Type paymentType)
		{
			await _paymentTypeRepository.Update(paymentType);
			return paymentType;
		}
	}
}
