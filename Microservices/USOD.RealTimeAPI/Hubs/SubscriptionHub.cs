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

		public async Task Subscribe(int fund_id)
		{
			await Groups.AddToGroupAsync(Context.ConnectionId, fund_id.ToString());
		}

		public async Task Unsubscribe(int fund_id)
		{
			await Groups.RemoveFromGroupAsync(Context.ConnectionId, fund_id.ToString());
		}
	}

	public interface ISubClient
	{
		Task ReceiveMessage(string message);
	}
}
