using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EliteReporter.Models
{
    [Serializable]
    public class EDProfile
    {
        public string SystemName { get; set; }
        public string PortName { get; set; }

        [JsonIgnore]
        public string CommanderName { get; set; }

        public override string ToString()
        {
            return string.Format("{0}/{1}", SystemName, PortName);
        }
    }
}
