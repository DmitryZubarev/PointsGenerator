
namespace Domain.ConfigOptions
{
    public class KafkaOptions
    {
        public ProducerOptions ProducerConfig { get; set; }
        public string Topic { get; set; }
    }
}
