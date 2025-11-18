using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PointsGenerator.Configs
{
    public class KafkaSettings
    {
        public string BootstrapServers { get; set; } = "localhost:9092";
        public string ClientId { get; set; } = "object-generator";
        public string Topic { get; set; } = "objects-topic";
    }
}
