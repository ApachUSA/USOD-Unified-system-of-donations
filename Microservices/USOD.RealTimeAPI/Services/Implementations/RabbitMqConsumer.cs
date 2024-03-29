﻿using Microsoft.AspNetCore.Connections;
using Microsoft.AspNetCore.SignalR;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;
using USOD.RealTimeAPI.Hubs;

namespace USOD.RealTimeAPI.Services.Implementations
{
	public class RabbitMqConsumer : BackgroundService
	{
		private readonly IHubContext<SubscriptionHub, ISubClient> _hubContext;

		public RabbitMqConsumer(IHubContext<SubscriptionHub, ISubClient> hubContext)
		{
			_hubContext = hubContext;
		}

		protected async override Task ExecuteAsync(CancellationToken stoppingToken)
		{
			var factory = new ConnectionFactory()
			{
				HostName = "RabbitMQ",
				Port = 5672,
				UserName = "admin",
				Password = "admin"
			};


			using var connection = factory.CreateConnection();
			using var channel = connection.CreateModel();

			channel.QueueDeclare(
				queue: "ProjectNotification",
				durable: true,
				exclusive: false,
				autoDelete: false,
				arguments: null);

			var consumer = new EventingBasicConsumer(channel);
			consumer.Received += async (model, ea) =>
			{
				await SendMessage(ea.Body.ToArray());
			};

			channel.BasicConsume(queue: "ProjectNotification",
								 autoAck: true,
								 consumer: consumer);

			await Task.Delay(Timeout.Infinite, stoppingToken);
		}

		private async Task SendMessage(byte[] array)
		{
			var message = Encoding.UTF8.GetString(array);

			await _hubContext.Clients.Group(message).ReceiveMessage(message);

		}
	}
}
