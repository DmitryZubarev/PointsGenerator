using Confluent.Kafka;
using Domain.Interfaces.Kafka;
using Domain.Models;
using Domain.Options;
using Microsoft.Extensions.Options;
using System.Text.Json;


namespace Infrastructure.Kafka.Producers
{
    public class PointsMessageProducer : IBaseProducer<string, PointsMessage>
    {
        private readonly IProducer<string, PointsMessage> _producer;
        private const string Topic = "points";


        public PointsMessageProducer()
        {
            var config = new ProducerConfig() { BootstrapServers = "localhost:9092" };
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
            await _producer.ProduceAsync(Topic,
                new Message<string, PointsMessage>
                {
                    Key = message.SerialNumber,
                    Value = message
                });
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
