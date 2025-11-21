using Domain.Interfaces.Kafka;
using Domain.Models;
using Infrastructure.Kafka.Producers;
using static Confluent.Kafka.ConfigPropertyNames;


namespace PointsGenerator
{
    public class Worker : BackgroundService
    {
        private readonly ILogger<Worker> _logger;
        private readonly PointsMessageProducer _producer;
        private Random _random = new Random();

        private readonly List<int> _brigadeCodes = [1, 2, 3];
        private readonly List<string> _serialNumbers = ["serialNumber1", "serialNumber2", "serialNumber3"];
        private readonly List<Channel> _channels = [
            new Channel { Type = 1, NumSuffix = 1},
            new Channel { Type = 1, NumSuffix = 2 },
            new Channel { Type = 2, NumSuffix = 1},
            new Channel { Type = 2, NumSuffix = 2}
        ];

        public Worker(ILogger<Worker> logger, PointsMessageProducer producer)
        {
            _logger = logger;
            _producer = producer;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                var message = GenerateMessage();

                await _producer.ProduceAsync(message);

                //_logger.LogInformation(message.BrigadeCode.ToString() + " " + message.SerialNumber);

                await Task.Delay(3000, stoppingToken);
            }
        }


        private PointsMessage GenerateMessage()
        {
            var message = new PointsMessage();

            int idx = _random.Next(0, 2);
            int channelCount = _random.Next(1, 4);
            message.BrigadeCode = _brigadeCodes[idx];
            message.SerialNumber = _serialNumbers[idx];

            for (int i = 0; i < channelCount; i++)
            {
                var channel = _channels[i];
                var newChannel = new Channel
                {
                    Type = channel.Type,
                    NumSuffix = channel.NumSuffix,
                };
                newChannel.Value = _random.NextDouble() * 10;
                message.Channels.Add(newChannel);
            }

            message.Timestamp = DateTime.UtcNow;

            return message;
        }
    }
}
