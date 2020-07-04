using HowWear.Model;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace HowWear.ViewModel
{
    class ForecastWeatherAPI
    {
        public const string API_KEY = "04cc40b66589737f0ccd9a706eef375b";
        public const string BASE_URL = "https://api.openweathermap.org/data/2.5/forecast?q={0}&appid={1}&units=metric";

        public static ForecastWeatherInfo GetWeatherInformation(string cityName)
        {
            ForecastWeatherInfo result = new ForecastWeatherInfo();

            string url = string.Format(BASE_URL, cityName, API_KEY);

            using (HttpClient Client = new HttpClient())
            {
                var response = Client.GetAsync(url);
                string json = response.Result.Content.ReadAsStringAsync().Result;

                result = JsonConvert.DeserializeObject<ForecastWeatherInfo>(json);
            }
            return result;
        }
    }
}
