using System;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace StandardQueue
{
	class Program
	{
		private static readonly string QueueName = "StandardQueue_Example";

		private IConnection connection;
		private IModel model;

		static void Main()
		{
			var program = new Program();

			program.CreateQueue();

			program.SendMessage(new Message { Text = "Hello world!" });
			program.SendMessage(new Message { Text = "Hello strange world!" });

			program.Receive();
		}

		private void CreateQueue()
		{
			Console.Write("Creating queue... ");

			var factory = new ConnectionFactory
			{
				HostName = "localhost",
				UserName = "guest",
				Password = "guest"
			};

			connection = factory.CreateConnection();

			model = connection.CreateModel();
			model.QueueDeclare(
				queue: QueueName,
				durable: true,
				exclusive: false,
				autoDelete: false,
				arguments: null);

			Console.WriteLine("Done");
		}

		private void SendMessage(Message message)
		{
			Console.Write("Sending message... ");

			model.BasicPublish(
				exchange: string.Empty,
				routingKey: QueueName,
				basicProperties: null,
				body: message.Serialize());

			Console.WriteLine("Done");
		}

		private void Receive()
		{
			Console.WriteLine("Receiving messages:");

			var consumer = new EventingBasicConsumer(model);
			consumer.Received += (sender, e) =>
			{
				var message = e.Body.Deserialize<Message>();
				Console.WriteLine($"  {message.Text}");
			};

			model.BasicConsume(
				queue: QueueName,
				noAck: true,
				consumer: consumer);
		}
	}
}