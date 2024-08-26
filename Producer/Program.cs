using RabbitMQ.Client;
using System.Text;

// Get RabbitMQ connection details from environment variables
var rabbitMqHost = Environment.GetEnvironmentVariable("RABBITMQ_HOST") ?? "localhost";
var rabbitMqUser = Environment.GetEnvironmentVariable("RABBITMQ_USER") ?? "guest";
var rabbitMqPass = Environment.GetEnvironmentVariable("RABBITMQ_PASS") ?? "guest";

var factory = new ConnectionFactory() { 
    HostName = rabbitMqHost,
    UserName = rabbitMqUser,
    Password = rabbitMqPass,
};

using (var connection = factory.CreateConnection())
using (var channel = connection.CreateModel())
{
    channel.QueueDeclare(
        queue: "sample",
        durable: false,
        exclusive: false,
        autoDelete: false,
        arguments: null
    );

    string message = "This is sample message";

    var body = Encoding.UTF8.GetBytes(message);
    channel.BasicPublish(
        exchange: "",
        routingKey: "sample",
        basicProperties: null,
        body: body
    );

    Console.WriteLine($"[x] Sent a message : {message}");
}

Console.WriteLine("Press any key to exit.");
Console.ReadKey();