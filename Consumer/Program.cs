using RabbitMQ.Client.Events;
using RabbitMQ.Client;
using System.Text;

var factory = new ConnectionFactory() { HostName = "rabbitmq" };

var number = 0;

using (var connection = factory.CreateConnection())
using (var channel = connection.CreateModel())
{
    channel.QueueDeclare(
        queue: "sample",
        durable: false,
        exclusive: false,
        autoDelete: false,
        arguments: null);

    var consumer = new EventingBasicConsumer(channel);
    consumer.Received += (model, ea) =>
    {
        var body = ea.Body.ToArray();
        var message = Encoding.UTF8.GetString(body);

        Console.WriteLine("Number of message: " + ++number);
        Console.WriteLine($"[x] Received a message : {message}");
    };

    channel.BasicConsume(
        queue: "sample",
        autoAck: true,
        consumer: consumer);


    Console.WriteLine(" Press any key to exit.");
    Console.ReadKey();
}
