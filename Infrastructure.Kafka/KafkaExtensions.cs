using Domain.Interfaces.Kafka;
using Domain.Models;
using Infrastructure.Kafka.Producers;
using Microsoft.Extensions.DependencyInjection;


namespace Infrastructure.Kafka
{
    public static class KafkaExtensions
    {
        public static void AddKafka(this IServiceCollection services)
        {
            services.AddSingleton<PointsMessageProducer>();
        }
    }
}
