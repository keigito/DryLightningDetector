using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DryLightning.Data.Models
{
    // The list that contains the weather conditions. 
    public class Weather
    {
        public IList<WeatherCondition> weather { get; set; }
    }
}
