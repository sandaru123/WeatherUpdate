using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ServiceStack;
using WeatherUpdate.Api.DAL;
using WeatherUpdate.Api.Model.Weather;
using WeatherUpdate.Service.Interface;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WeatherUpdate.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WeatherController : ControllerBase
    {
        private readonly IWeatherRepository weatherRepository;

        public WeatherController(IWeatherRepository _weatherRepository)
        {
            weatherRepository = _weatherRepository;
        }

        // GET: api/<WeatherController>
        [HttpGet]
        public WeatherJson Get()
        {
            string url = "http://demo4567044.mockable.io/weather";

            // GET Json data from api & map to WeatherJson
            WeatherJson todo = url.GetJsonFromUrl().FromJson<WeatherJson>();
            return todo;
        }

        [Route("~/api/SaveWeatherDetailsAsync")]
        [HttpPost]
        public async Task<ActionResult<bool>> SaveWeatherDetailsAsync(WeatherModel weather)
        {
            var bl = await weatherRepository.CreateWeatherAsync(weather);

            if (bl)
            {
                return Ok(new { bl, Message = "Success" });
            }

            return BadRequest(new { bl, Message = "Unsuccessfull" });
        }


        [Route("~/api/GetAllWeatherAsync")]
        [HttpGet]
        public async Task<ActionResult<List<Weather>>> GetAllWeatherAsync()
        {
            var weathers = await weatherRepository.GetAllWeatherAsync();

            if (weathers.Count != 0)
            {
                return Ok(new { weathers, Message = "Success" });
            }

            return BadRequest(new { Message = "Unsuccessfull" });
        }


    }
}
