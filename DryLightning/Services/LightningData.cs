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
    public class LightningData
    {
        static string _url = "http://lightningmaps.org/live/"; // Setting the url for the external lightning strike database

        // A method to obtain lightning strike data
        public async Task<LightningList> GetLightningDataAsync()
        {
            HttpClient client = new HttpClient();
            HttpResponseMessage response = await client.GetAsync(_url); 
            response.EnsureSuccessStatusCode();

            string results = await response.Content.ReadAsStringAsync();

            LightningList lightningDataList = JsonConvert.DeserializeObject<LightningList>(results); // Converting the JSON results to the LightnignList object

            return lightningDataList;
        }
    }
}