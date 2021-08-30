using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WeatherUpdate.Api.Model.Weather
{
    public class WeatherModel
    {
        public int humidity { get; set; }
        public int temperature { get; set; }
        public int min_temperature { get; set; }
        public int max_temperature { get; set; }
    }

    public class WeatherGetModel
    {
        public int WeatherId { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public int? humidity { get; set; }
        public int? temperature { get; set; }
        public int? MinTemp { get; set; }
        public int? MaxTemp { get; set; }
    }
}
