using System;
using System.Collections.Generic;

namespace WeatherUpdate.Api.DAL
{
    public partial class Weather
    {
        public int WeatherId { get; set; }
        public TimeSpan? UpdatedTime { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public int? Humidity { get; set; }
        public int? Temperature { get; set; }
        public int? MinTemp { get; set; }
        public int? MaxTemp { get; set; }
    }
}
