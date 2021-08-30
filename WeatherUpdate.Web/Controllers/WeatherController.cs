using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using ServiceStack;
using Newtonsoft.Json;
using WeatherUpdate.Web.Models.Weather;
using Nancy.Json;

namespace WeatherUpdate.Web.Controllers
{
    public class WeatherController : Controller
    {
        // GET: WeatherController
        public ActionResult Index()
        {
            return View();
        }

      

        public ActionResult GetAllWeatherData2()
        {
            List<WeatherModel> weatherUrl = new List<WeatherModel>();

            string url = "https://localhost:44378/api/GetAllWeatherAsync";

            // GET Json data from api & map to WeatherJson
            weatherUrl = url.GetJsonFromUrl().FromJson<List<WeatherModel>>();

            return Json(new { data =weatherUrl });

        }

        public async Task<ActionResult> GetAllWeatherData()
        {
            List<WeatherModel> weatherUrl = new List<WeatherModel>();
            List<WeatherModel> newList = new List<WeatherModel>();

            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.GetAsync("https://localhost:44378/api/GetAllWeatherAsync"))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    weatherUrl = JsonConvert.DeserializeObject<List<WeatherModel>>(apiResponse);


                    foreach (var obj in weatherUrl)
                    {
                        DateTime dt = obj.UpdatedDate.HasValue ? obj.UpdatedDate.Value : DateTime.MinValue;
                      
                        obj.UpdatedTimestr = dt.ToLongTimeString();  // includes leading zeros
                        obj.UpdateDatestr = dt.ToLongDateString();  // includes leading zeros
                    }

                    newList = weatherUrl.OrderByDescending(x => x.WeatherId).ToList();

                }
            }
            // return Json(weatherUrl);
            return Json(new { data = newList });
        }

        public async Task<ActionResult> AddWeatherData1()
        {
            bool res = true;
            WeatherAddModel weatherAddModel = new WeatherAddModel();

            weatherAddModel.humidity = 12;
            weatherAddModel.temperature = 12;
            weatherAddModel.min_temperature = 12;
            weatherAddModel.max_temperature = 12;

            using (var httpClient = new HttpClient())
            {
                //var weatherM = JsonSerializer.Serialize(weatherAddModel);
                //var requestContent = new StringContent(weatherM, Encoding.UTF8, "application/json");

                using (var response = await httpClient.GetAsync("https://localhost:44378/api/SaveWeatherDetailsAsync" + weatherAddModel))
                {
                    if (response.StatusCode == System.Net.HttpStatusCode.OK)
                    {
                        string apiResponse = await response.Content.ReadAsStringAsync();
                        res = JsonConvert.DeserializeObject<bool>(apiResponse);
                    }
                    else
                        ViewBag.StatusCode = response.StatusCode;
                }
            }
            return Json(new { data = res });
        }


        public async Task<ActionResult<bool>> AddWeatherdata(WeatherAddModel weatherModel)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://localhost:44378/");
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));


                var response = client.PostAsync("api/SaveWeatherDetailsAsync", new StringContent(new JavaScriptSerializer().Serialize(weatherModel), Encoding.UTF8, "application/json")).Result;

                if (response.IsSuccessStatusCode)
                {
                    
                    return true;
                }
                return false;
            }
        }


    }
}
