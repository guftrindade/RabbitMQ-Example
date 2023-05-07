using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;
using System.Text.Json;
using Rabbit.Models.Entities;

namespace Rabbit.Consumer
{
    internal static class Program
    {
        private static void Main(string[] args)
        {
            var factory = new ConnectionFactory()
            {
                HostName = "localhost",
                UserName = "guest",
                Password = "guest"
            };

            using (var connection = factory.CreateConnection())
            using (var channel = connection.CreateModel())
            {
                channel.QueueDeclare(queue: "rabbitMensagesQueue",
                                     durable: false,
                                     exclusive: false,
                                     autoDelete: false,
                                     arguments: null);

                var consumer = new EventingBasicConsumer(channel);

                consumer.Received += (model, ea) =>
                {
                    var body = ea.Body.ToArray();
                    var json = Encoding.UTF8.GetString(body);

                    RabbitMensagem mensagem = JsonSerializer.Deserialize<RabbitMensagem>(json);
                    Thread.Sleep(1000);
                    Console.WriteLine($"Titulo: {mensagem.Titulo}; Texto={mensagem.Texto}; Id={mensagem.Id}");
                };
                channel.BasicConsume(queue: "rabbitMensagesQueue",
                                     autoAck: true,
                                     consumer: consumer);

                Console.WriteLine(" Press [enter] to exit.");
                Console.ReadLine();
            }
        }
    }
}