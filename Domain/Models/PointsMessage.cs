
using Domain.Interfaces.Common;

namespace Domain.Models
{
    public class PointsMessage : IMessage
    {
        public int BrigadeCode { get; set; }
        public string SerialNumber { get; set; }
        public DateTime Timestamp { get; set; }

        public Channel[] Channels { get; set; }
    }
}
