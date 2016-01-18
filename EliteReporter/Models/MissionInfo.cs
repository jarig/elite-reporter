using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EliteReporter.Models
{
    [Serializable]
    public class MissionInfo
    {
        public string MissionName { get; set; }
        //public string StationName { get; set; }

        public DateTime MissionTakenDateTime { get; set; }
        public DateTime MissionFinishedDateTime { get; set; }
        public EDProfile MissionTakenEDProfile { get; set; }
        public EDProfile MissionFinishedEDProfile { get; set; }

        [NonSerialized]
        public List<Image> Images = new List<Image>();
    }
}
