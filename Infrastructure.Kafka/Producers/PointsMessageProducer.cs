using Confluent.Kafka;
using Domain.Interfaces.Kafka;
using Domain.Models;
using Domain.Options;
using Microsoft.Extensions.Options;


namespace Infrastructure.Kafka.Producers
{
    public class PointsMessageProducer : IBaseProducer<string, PointsMessage>
    {
        private readonly string _topic;
        private readonly ProducerConfig _producerConfig;
        private readonly IProducer<string, PointsMessage> _producer;


        public PointsMessageProducer(
            IOptions<KafkaOptions> config,
            IProducer<string, PointsMessage> producer)
        {
            _producer = producer;

            //Здесь нужно достать конфиг из конфигурации и смапить с конфигом кафки
            _producerConfig = new ProducerConfig() { BootstrapServers = "localhost:9092" };/*config.Value.ProducerConfig*/;
            _topic = config.Value.Topic;

            _producer = new ProducerBuilder<string, PointsMessage>(_producerConfig)
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
