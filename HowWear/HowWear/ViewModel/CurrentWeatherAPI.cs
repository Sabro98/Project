using HowWear.Model;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;

namespace HowWear.ViewModel
{
    class CurrentWeatherAPI
    {
        public const string API_KEY = "04cc40b66589737f0ccd9a706eef375b";
        public const string BASE_URL = "http://api.openweathermap.org/data/2.5/weather?q={0}&APPID={1}&units=metric";

        public static CurrentWeatherInfo GetWeatherInformation(string cityName)
        {
            CurrentWeatherInfo result = new CurrentWeatherInfo();

            string url = string.Format(BASE_URL, cityName, API_KEY);

            using (HttpClient Client = new HttpClient())
            {
                var response = Client.GetAsync(url);
                string json = response.Result.Content.ReadAsStringAsync().Result;

                result = JsonConvert.DeserializeObject<CurrentWeatherInfo>(json);
            }
            return result;
        }
    }
}
