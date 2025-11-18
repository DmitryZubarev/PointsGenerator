
namespace Domain.Configs
{
    public class ProducerConfig
    {
        public string Host { get; set; }
        public int Port { get; set; }
        public string Topic { get; set; }

        public string FullAddress
        {
            get
            {
                return $"{Host}:{Port}";
            }
        }
    }
}
