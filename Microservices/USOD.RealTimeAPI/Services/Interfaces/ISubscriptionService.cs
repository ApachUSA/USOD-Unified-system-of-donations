using RealTime_Library;

namespace USOD.RealTimeAPI.Services.Interfaces
{
	public interface ISubscriptionService
	{
		Task<List<Subscription>> GetAsync(int donor_id);

		Task<Subscription?> GetByIdAsync(int sub_id);

		Task<Subscription> SubscribeAsync(Subscription comment);

		Task<bool> UnsubscribeAsync(Subscription comment);
	}
}
