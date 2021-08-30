using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WeatherUpdate.Api.DAL;
using WeatherUpdate.Api.Model.Weather;

namespace WeatherUpdate.Service.Interface
{
    public interface IWeatherRepository
    {
        Task UpdateWeatherDetailsAsync();
        Task<bool> CreateWeatherAsync(WeatherModel weatherModel);

        Task<List<WeatherGetModel>> GetAllWeatherAsync();
    }
}
