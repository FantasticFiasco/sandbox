using System;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using Shared;

namespace WorkerQueue
{
	class Program
	{
		private static readonly string QueueName = "WorkerQueue_Example";

		private IConnection connection;
		private IModel model;

		static void Main()
		{
			var program = new Program();

			program.CreateQueue();

			program.Produce(new Message { Text = "Hello world!" });
			program.Produce(new Message { Text = "Hello strange world!" });

			program.Consume();
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

		private void Produce(Message message)
		{
			Console.Write("Sending message... ");

			model.BasicPublish(
				exchange: string.Empty,
				routingKey: QueueName,
				basicProperties: null,
				body: message.Serialize());

			Console.WriteLine("Done");
		}

		private void Consume()
		{
			Console.WriteLine("Receiving messages:");

			model.BasicQos(
				prefetchSize: 0,
				prefetchCount: 1,
				global: false);

			var consumer = new EventingBasicConsumer(model);
			consumer.Received += (sender, e) =>
			{
				var message = e.Body.Deserialize<Message>();
				Console.WriteLine($"  {message.Text}");

				model.BasicAck(
					deliveryTag: e.DeliveryTag,
					multiple: false);
			};

			model.BasicConsume(
				queue: QueueName,
				noAck: true,
				consumer: consumer);
		}
	}
}