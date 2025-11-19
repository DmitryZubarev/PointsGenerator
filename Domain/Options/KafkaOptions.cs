
namespace Domain.Options
{
    public class KafkaOptions
    {
        public ProducerOptions ProducerConfig { get; set; }
        public string Topic { get; set; }
    }
}
