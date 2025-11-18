using Confluent.Kafka;
using Domain.Interfaces.Kafka;
using Domain.Models;
using System.Net;


namespace Infrastructure.Kafka.Producers
{
    public class PointsMessageProducer : IBaseProducer<string, PointsMessage>
    {
        private readonly string _topic;
        private readonly ProducerConfig _config;
        private readonly IProducer<string, PointsMessage> _producer;


        public PointsMessageProducer(IProducer<string, PointsMessage> producer)
        {
            _producer = producer;

            _config = new ProducerConfig
            {
                BootstrapServers = "localhost:9092",
            };
            _topic = "Points";

            _producer = new ProducerBuilder<string, PointsMessage>(_config)
                .Build();
        }


        public void Dispose()
        {
            _producer?.Dispose();
        }

        public async Task ProduceAsync(PointsMessage message)
        {
            await _producer.ProduceAsync(
                _topic,
                new Message<string, PointsMessage>
                {
                    Key = message.SerialNumber,
                    Value = message
                });
        }
    }
}
