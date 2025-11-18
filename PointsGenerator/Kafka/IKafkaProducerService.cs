using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PointsGenerator.Kafka
{
    public interface IKafkaProducerService
    {
        Task ProduceAsync(string message);
    }
}
