using Confluent.Kafka;
using Microsoft.Extensions.Options;
using PointsGenerator;
using PointsGenerator.Configs;
using PointsGenerator.Kafka;

var builder = Host.CreateApplicationBuilder(args);
builder.Services.AddHostedService<Worker>();

builder.Services.Configure<KafkaSettings>(
    builder.Configuration.GetSection("Kafka"));

// Регистрация Kafka Producer
builder.Services.AddSingleton<IProducer<Null, string>>(provider =>
{
    var config = provider.GetRequiredService<IOptions<KafkaSettings>>().Value;

    var producerConfig = new ProducerConfig
    {
        BootstrapServers = config.BootstrapServers,
        ClientId = config.ClientId,
        Acks = Acks.All,
        MessageSendMaxRetries = 3,
        RetryBackoffMs = 1000,
        LingerMs = 5,
        BatchSize = 32768
    };

    return new ProducerBuilder<Null, string>(producerConfig).Build();
});

builder.Services.AddSingleton<IKafkaProducerService, KafkaProducerService>();

var host = builder.Build();
host.Run();
