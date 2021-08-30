using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WeatherUpdate.Api.DAL;
using WeatherUpdate.Api.Helper;
using WeatherUpdate.Api.Model.Weather;
using WeatherUpdate.Service.Interface;

namespace WeatherUpdate.Service
{
    public class WeatherRepository : IWeatherRepository
    {
        private readonly WeatherDbContext DBContext;

        public WeatherRepository(WeatherDbContext _DBContext)
        {
            DBContext = _DBContext;

        }

        public async Task UpdateWeatherDetailsAsync()
        {
            try
            {
                WeatherJson weatherJson = GetWeather.GetWeatherFromURL();

                Weather weather = new Weather();
                weather.Humidity = weatherJson.humidity;
                weather.Temperature = weatherJson.temperature;
                weather.MaxTemp = weatherJson.max_temperature;
                weather.MinTemp = weatherJson.min_temperature;

                weather.UpdatedDate = DateTime.Now;
                weather.UpdatedTime = DateTime.Now.TimeOfDay;

                await DBContext.Weather.AddAsync(weather);
                await DBContext.SaveChangesAsync();
            }
            catch (Exception e)
            {

                throw e;
            }

        }

        /// <summary>
        /// create weather record by user
        /// </summary>
        /// <param name="weatherModel"></param>
        /// <returns></returns>
        public async Task<bool> CreateWeatherAsync(WeatherModel weatherModel)
        {
            try
            {
                Weather weather = new Weather();

                weather.Humidity = weatherModel.humidity;
                weather.Temperature = weatherModel.temperature;
                weather.MinTemp = weatherModel.min_temperature;
                weather.MaxTemp = weatherModel.max_temperature;

                weather.UpdatedDate = DateTime.Now;
                weather.UpdatedTime = DateTime.Now.TimeOfDay;

                await DBContext.Weather.AddAsync(weather);
                await DBContext.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {

                throw ex;
            }

        }

        public async Task<List<WeatherGetModel>> GetAllWeatherAsync()
        {
            try
            {
                List<Weather> weather = new List<Weather>();
                List<WeatherGetModel> weatherModel = new List<WeatherGetModel>();
                weather = await DBContext.Weather.OrderByDescending(c => c.WeatherId).ToListAsync();
                if (weather.Count != 0)
                {
                    foreach (var obj in weather)
                    {
                       WeatherGetModel getModel = new WeatherGetModel();
                        getModel.WeatherId = obj.WeatherId;
                        getModel.UpdatedDate = obj.UpdatedDate;
                        getModel.temperature = obj.Temperature;
                        getModel.humidity = obj.Humidity;
                        getModel.MinTemp = obj.MinTemp;
                        getModel.MaxTemp = obj.MaxTemp;

                        weatherModel.Add(getModel);
                    }

                    return weatherModel;
                }

                return weatherModel;

            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
    }
}
