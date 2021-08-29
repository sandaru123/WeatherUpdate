using ServiceStack;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WeatherUpdate.Api.Model.Weather;

namespace WeatherUpdate.Api.Helper
{
    public class GetWeather
    {
        //retrieve weather data from url
        public static WeatherJson GetWeatherFromURL()
        {
            WeatherJson weatherUrl = new WeatherJson();

            string url = "http://demo4567044.mockable.io/weather";

            // GET Json data from api & map to WeatherJson
            weatherUrl = url.GetJsonFromUrl().FromJson<WeatherJson>();

            return weatherUrl;
        }
    }
}
