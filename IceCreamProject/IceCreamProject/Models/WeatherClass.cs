using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IceCreamProject.Models
{
    public class WeatherClass
    {
        public Main CheckWeather(string city)
        {
            Main result = null;
            string apiKey = "efe83d986e7db538087a0a3355daeff8";

            var client = new RestClient("https://api.openweathermap.org/data/2.5/weather");
            client.Timeout = -1;

            var request = new RestRequest(Method.GET);
            request.AddParameter("q", city);
            request.AddParameter("appid", apiKey);
            request.AddParameter("units", "metric");
            IRestResponse response = client.Execute(request);
            //result = ConvertToDictionary(response.Content);
            Root TheTags = JsonConvert.DeserializeObject<Root>(response.Content);
            //Console.Write(response.Content);
            // Console.ReadLine();
            return TheTags.main;
        }
    }
}
