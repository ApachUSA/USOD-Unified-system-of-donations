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

		public async Task<Subscription?> GetByIdAsync(int sub_id)
		{
			return await _subscriptionRepository.Get().FirstOrDefaultAsync(x => x.Subscription_ID == sub_id);
		}

		public async Task<Subscription> SubscribeAsync(Subscription comment)
		{
			await _subscriptionRepository.Create(comment);
			return comment;
		}

		public async Task<bool> UnsubscribeAsync(Subscription comment)
		{
			await _subscriptionRepository.Delete(comment);
			return true;
		}
	}
}
