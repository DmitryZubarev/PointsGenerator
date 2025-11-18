namespace PointsGenerator.Models
{
    public class PointsMessage
    {
        public int BrigadeCode { get; set; }
        public string SerialNumber { get; set; }
        public List<Channel> Channels { get; set; } = new List<Channel>();
        public DateTime Timestamp { get; set; }


        public override string ToString()
        {
            string messageString = "";
            messageString += $"BrigadeCode: {BrigadeCode}\n" +
                $"SerialNumber: {SerialNumber}\n" +
                $"Timestamp: {Timestamp.ToString()}\n";
            for (int i = 0; i < Channels.Count; i++)
            {
                messageString += $"Channel{i + 1}: {Channels[i].ToString()}\n";
            }

            return messageString;
        }
    }
}
