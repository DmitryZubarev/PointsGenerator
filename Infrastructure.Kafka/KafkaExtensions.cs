using Infrastructure.Kafka.Producers;
using Microsoft.Extensions.DependencyInjection;


namespace Infrastructure.Kafka
{
    public static class KafkaExtensions
    {
        public static void AddKafka(IServiceCollection services)
        {
            services.AddSingleton<PointsMessageProducer>();
        }
    }
}
