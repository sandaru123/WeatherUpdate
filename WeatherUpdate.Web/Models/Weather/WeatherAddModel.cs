using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WeatherUpdate.Web.Models.Weather
{
    public class WeatherAddModel
    {
        public int humidity { get; set; }
        public int temperature { get; set; }
        public int min_temperature { get; set; }
        public int max_temperature { get; set; }
    }
}
