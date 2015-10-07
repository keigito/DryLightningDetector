using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DryLightning.Data.Models
{
    // Model for individual lightning strike data fetched from lightningmaps.org. Only the latitude, longitude and time properties are taken.
    public class Lightning
    {
        public float lat { get; set; }
        public float lon { get; set; }
        public int Time { get; set; }
    }
}
