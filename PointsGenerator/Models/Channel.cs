using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PointsGenerator.Models
{
    public class Channel
    {
        public int Type { get; set; }
        public int NumSuffix {  get; set; }
        public double? Value { get; set; }

        public Channel Clone()
        {
            return new Channel
            {
                Type = this.Type,
                NumSuffix = this.NumSuffix,
                Value = this.Value
            };
        }

        public override string ToString()
        {
            string channelString = $"Type - {Type}, NumSuffix - {NumSuffix}, Value - {Value}";
            return channelString;
        }
    }
}
