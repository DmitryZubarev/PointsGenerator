using Confluent.Kafka;
using Domain.Interfaces.Kafka;
using Domain.Models;
using Domain.Options;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System.Text.Json;


namespace Infrastructure.Kafka.Producers
{
    public class PointsMessageProducer : IBaseProducer<string, PointsMessage>
    {
        private readonly ILogger<PointsMessageProducer> _logger;
        private readonly IProducer<string, PointsMessage> _producer;
        private const string Topic = "points";


        public PointsMessageProducer(ILogger<PointsMessageProducer> logger)
        {
            _logger = logger;
            var config = new ProducerConfig() { BootstrapServers = "kafka:9092" };
            _producer = new ProducerBuilder<string, PointsMessage>(config)
                .SetValueSerializer(new PointsMessageSerializer())
                .Build();
        }


        public void Dispose()
        {
            _producer?.Dispose();
        }

        public async Task ProduceAsync(PointsMessage message)
        {
            var res = await _producer.ProduceAsync(Topic,
                new Message<string, PointsMessage>
                {
                    Key = message.SerialNumber,
                    Value = message
                });
            _logger.LogInformation(res.Status.ToString());
        }

        public class PointsMessageSerializer : ISerializer<PointsMessage>
        {
            public byte[] Serialize(PointsMessage data, SerializationContext context)
            {
                return JsonSerializer.SerializeToUtf8Bytes(data);
            }
        }
    }
}
