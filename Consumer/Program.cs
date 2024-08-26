using RabbitMQ.Client.Events;
using RabbitMQ.Client;
using System.Text;

// Get RabbitMQ connection details from environment variables
var rabbitMqHost = Environment.GetEnvironmentVariable("RABBITMQ_HOST") ?? "localhost";
var rabbitMqUser = Environment.GetEnvironmentVariable("RABBITMQ_USER") ?? "guest";
var rabbitMqPass = Environment.GetEnvironmentVariable("RABBITMQ_PASS") ?? "guest";

var factory = new ConnectionFactory()
{
    HostName = rabbitMqHost,
    UserName = rabbitMqUser,
    Password = rabbitMqPass,
};


Console.WriteLine(" Press any key to star reading messages.");
Console.ReadKey();

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

Console.WriteLine(" Press any key to exit.");
Console.ReadKey();
