using Rabbit.Models.Entities;
using Rabbit.Repositories.Interfaces;
using RabbitMQ.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Rabbit.Repositories
{
    public class RabbitMensagemRepository : IRabbitMensagemRepository
    {
        public void SendMensagem(RabbitMensagem mensagem)
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

                string json = JsonSerializer.Serialize(mensagem);
                var body = Encoding.UTF8.GetBytes(json);

                channel.BasicPublish(exchange: "",
                                     routingKey: "rabbitMensagesQueue",
                                     basicProperties: null,
                                     body: body);
            }
        }
    }
}
