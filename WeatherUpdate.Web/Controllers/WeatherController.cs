using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using ServiceStack;
using WeatherUpdate.Web.Models.Weather;

namespace WeatherUpdate.Web.Controllers
{
    public class WeatherController : Controller
    {
        // GET: WeatherController
        public ActionResult Index()
        {
            return View();
        }

        // GET: WeatherController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: WeatherController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: WeatherController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: WeatherController/Edit/5
        public ActionResult Edit(int id)
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

            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.GetAsync("https://localhost:44378/api/GetAllWeatherAsync"))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    weatherUrl = JsonConvert.DeserializeObject<List<WeatherModel>>(apiResponse);
                }
            }
            // return Json(weatherUrl);
            return Json(new { data = weatherUrl });
        }


    }
}
