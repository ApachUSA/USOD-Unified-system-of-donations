using Microsoft.AspNetCore.SignalR;
using RealTime_Library;
using USOD.RealTimeAPI.Services.Interfaces;

namespace USOD.RealTimeAPI.Hubs
{
	public class CommentHub : Hub<ICommentClient>
	{
		public async Task JoinProject(string project_id)
		{
			await Groups.AddToGroupAsync(Context.ConnectionId, project_id);
		}
			
		public async Task SendMessage(Project_Comment comment)
		{
			await Clients.All.ReceiveComment(comment);
		}
	}

	public interface ICommentClient
	{
		Task ReceiveMessage(string message);

		Task ReceiveComment(Project_Comment project_Comment);
	}
}
