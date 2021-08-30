using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WeatherUpdate.Web.Models.Weather
{
    public class WeatherModel
    {
        public int WeatherId { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public int? Humidity { get; set; }
        public int? Temperature { get; set; }
        public int? MinTemp { get; set; }
        public int? MaxTemp { get; set; }
    }
}
