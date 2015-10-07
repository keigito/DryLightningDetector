using DryLightning.Data.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;

namespace DryLightning.Services
{
    public class WeatherData
    {
        static string _urlBase = "http://api.openweathermap.org/data/2.5/weather?"; // The base url for the openweathermap api.

        // A method to fetch weather condition data from openweathermap.org
        public async Task<Weather> GetWeatherConditionAsync(float lat, float lon) // An asynchronous method to avoid delay while accessing external API data source.
        {
            string _url = _urlBase + "lat=" + lat + "&lon=" + lon;
            HttpClient client = new HttpClient();
            HttpResponseMessage response = await client.GetAsync(_url);
            response.EnsureSuccessStatusCode();
            string results = await response.Content.ReadAsStringAsync();

            Weather weatherCondition = JsonConvert.DeserializeObject<Weather>(results); // Converting JSON results into a Weather object

            return weatherCondition;
        }
    }
}