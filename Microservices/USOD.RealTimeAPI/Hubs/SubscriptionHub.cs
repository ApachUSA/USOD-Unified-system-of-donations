using Microsoft.AspNetCore.SignalR;
using RealTime_Library;
using USOD.RealTimeAPI.Services.Interfaces;

namespace USOD.RealTimeAPI.Hubs
{
	public class SubscriptionHub : Hub<ISubClient>
	{
		private readonly ISubscriptionService _subscriptionService;
		private readonly ILogger<SubscriptionHub> _logger;

		public SubscriptionHub(ISubscriptionService subscriptionService, ILogger<SubscriptionHub> logger)
		{
			_subscriptionService = subscriptionService;
			_logger = logger;
		}

		public async Task ConnectFund(int donor_id)
		{
			var subs = await _subscriptionService.GetAsync(donor_id);
			foreach (var subscription in subs)
			{
				await Groups.AddToGroupAsync(Context.ConnectionId, subscription.Fund_ID.ToString());	
			}	
		}

		public async Task Subscribe(Subscription subscription)
		{
			try
			{
				await _subscriptionService.SubscribeAsync(subscription);
				await Groups.AddToGroupAsync(Context.ConnectionId, subscription.Fund_ID.ToString());
			}
			catch (Exception ex)
			{
				_logger.LogError("Exception: {ex}", ex.Message);
			}
			
		}

		public async Task Unsubscribe(int sub_id)
		{
			var sub = await _subscriptionService.GetByIdAsync(sub_id);
			if(sub != null)
			{
				try
				{
					await _subscriptionService.UnsubscribeAsync(sub);
					await Groups.RemoveFromGroupAsync(Context.ConnectionId, sub.Fund_ID.ToString());
				}
				catch (Exception ex)
				{
					_logger.LogError("Exception: {ex}", ex.Message);
				}
			}
		}
	}

	public interface ISubClient
	{
		Task ReceiveMessage(string message);
	}
}
