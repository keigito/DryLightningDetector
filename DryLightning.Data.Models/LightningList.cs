using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DryLightning.Data.Models
{
    // List of lightning strike data. The lightningmaps.org web site uses a name "d" for an array that contains the lightning strike data, which needs to be identical to the IList<Lightning> property name.  
    public class LightningList
    {
        public IList<Lightning> d { get; set; }
    }
}
