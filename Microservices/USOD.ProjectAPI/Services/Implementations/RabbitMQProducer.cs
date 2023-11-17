using RabbitMQ.Client;
using System.Text;
using USOD.ProjectAPI.Services.Interfaces;

namespace USOD.ProjectAPI.Services.Implementations
{
	public class RabbitMQProducer : IMessageProducer
	{
		public void SendMessage(string message)
		{
			var factory = new ConnectionFactory()
			{
				HostName = "localhost",
				Port = 5672,
				UserName = "admin",
				Password = "admin"
			};

			var connection = factory.CreateConnection();

			using var channel = connection.CreateModel();

			channel.QueueDeclare(
				queue: "ProjectNotification",
				durable: true,
				exclusive: false,
				autoDelete: false,
				arguments: null);

			channel.BasicPublish(
				exchange: "Services",
				routingKey: "Notify",
				basicProperties: null,
				mandatory: false,
				body: Encoding.UTF8.GetBytes(message)
				);
		}
	}
}
