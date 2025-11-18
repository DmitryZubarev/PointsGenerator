using Confluent.Kafka;
using Microsoft.Extensions.Options;
using PointsGenerator.Configs;
using PointsGenerator.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PointsGenerator.Kafka
{
    public class KafkaProducerService : IKafkaProducerService, IDisposable
    {
        private readonly IProducer<Null, string> _producer;
        private readonly string _topic;
        private readonly ILogger<KafkaProducerService> _logger;

        public KafkaProducerService(
            IProducer<Null, string> producer,
            IOptions<KafkaSettings> settings,
            ILogger<KafkaProducerService> logger)
        {
            _producer = producer;
            _topic = settings.Value.Topic;
            _logger = logger;
        }

        public void Dispose()
        {
            _producer?.Flush(TimeSpan.FromSeconds(10));
            _producer?.Dispose();
        }

        public async Task ProduceAsync(string message)
        {
            try
            {
                var kafkaMessage = new Message<Null, string>
                {
                    Value = message.ToString()
                };

                var deliveryResult = await _producer.ProduceAsync(_topic, kafkaMessage);

                _logger.LogInformation(
                    "Message delivered to {Topic} [{Partition}] @ {Offset}",
                    deliveryResult.Topic,
                    deliveryResult.Partition,
                    deliveryResult.Offset);
            }
            catch (ProduceException<Null, string> ex)
            {
                _logger.LogError(ex, "Failed to deliver message to Kafka");
                throw;
            }
        }
    }
}
