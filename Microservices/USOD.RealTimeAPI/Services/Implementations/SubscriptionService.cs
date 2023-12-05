using Microsoft.EntityFrameworkCore;
using RealTime_Library;
using USOD.RealTimeAPI.Repositories.Interfaces;
using USOD.RealTimeAPI.Services.Interfaces;

namespace USOD.RealTimeAPI.Services.Implementations
{
	public class SubscriptionService : ISubscriptionService
	{
		private readonly IBaseRepository<Subscription> _subscriptionRepository;

		public SubscriptionService(IBaseRepository<Subscription> subscriptionRepository)
		{
			_subscriptionRepository = subscriptionRepository;
		}

		public async Task<List<Subscription>> GetAsync(int donor_id)
		{
			return await _subscriptionRepository.Get().Where(x => x.Donor_ID == donor_id).ToListAsync();
		}

		public async Task<Subscription?> GetByDonorAsync(Subscription sub)
		{
			return await _subscriptionRepository.Get().Where(x => x.Donor_ID == sub.Donor_ID && x.Fund_ID == sub.Fund_ID).FirstOrDefaultAsync();
		}

		public async Task<List<Subscription>> GetByFundAsync(int fund_id)
		{
			return await _subscriptionRepository.Get().Where(x => x.Fund_ID == fund_id).ToListAsync();
		}

		public async Task<Subscription?> GetByIdAsync(int sub_id)
		{
			return await _subscriptionRepository.Get().FirstOrDefaultAsync(x => x.Subscription_ID == sub_id);
		}

		public async Task<Subscription> SubscribeAsync(Subscription sub)
		{
			await _subscriptionRepository.Create(sub);
			return sub;
		}

		public async Task<bool> UnsubscribeAsync(Subscription sub)
		{
			await _subscriptionRepository.Delete(sub);
			return true;
		}
	}
}
