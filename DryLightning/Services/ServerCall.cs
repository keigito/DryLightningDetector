using System;
using System.Collections.Generic;
using System.Linq;
//using System.Threading;
using System.Timers;
using System.Threading.Tasks;
using DryLightning.Data.Models;
using DryLightning.Repositories;

namespace DryLightning.Services
{
    // ServerCall class contains methods to obtain lightning strike and weather data from external API's, perform polling to fetch these data periodically, check if there were lightning strikes within the user-specified radius at each of the user-specified locations, and if the conditions match, sends an email to the user. It also records the last time an alert email was sent, and prevents email alert flooding in case there are many lightning strikes within the short period of time.
    public static class ServerCall
    {
        // Instantiating the classes required.
        static Timer timer;
        static LightningData lightningData = new LightningData();
        static WeatherData weatherData = new WeatherData();
        static EmailTimeRepository emailTime = new EmailTimeRepository();
        static SendEmail sendEmail = new SendEmail();
        static UserLocationRepository userLocation = new UserLocationRepository();

        // The timer function to start. Polling is done every 2 min.
        public static void Start()
        {
            timer = new Timer(120000);
            timer.Elapsed += timer_Elapsed;
            timer.Enabled = true;
        }

        // The timer_Elapsed method is called every 2 min. This method accesses the external API's, so it is made asynchronous to prevent any thread blocking. 
        static async void timer_Elapsed(object sender, ElapsedEventArgs e)
        {
            try
            {
                Task<LightningList> lightningResults = lightningData.GetLightningDataAsync();
                LightningList lightningLocationData = lightningResults.Result;                  // The results from the lightning strike database.
                IList<UserLocation> userLocationsList = userLocation.ListLocations().ToList();  // The list of User specified locations to be compared against the lightning strike locations. 
                foreach (var lightning in lightningLocationData.d)
                {
                    foreach (var userLocation in userLocationsList)
                    {
                        float userRad = userLocation.Radius;        // The detection threshold radius.
                        float userLat = userLocation.Latitude;      // The user-specified latitude.
                        float userLon = userLocation.Longitude;     // The user-specified longitude.
                        float lightningLat = lightning.lat;         // The latitude of the lightning strike
                        float lightningLon = lightning.lon;         // The longitude of the lightning strike

                        TimeSpan? timeDiff = DateTime.Now - userLocation.LastEmailTime;     // Computes the length of time since the last alert email was sent. 

                        //try {
                        if (timeDiff.Value.TotalMinutes > 59.00)    // Test if the email was send within the last 59 min.
                        {
                            double dist = Math.Pow(((userLat - lightningLat) * 68.9318), 2) + Math.Pow(((userLon - lightningLon) * Math.Cos(userLat) * 69.1742), 2);  // Computing the square of the distance between the user-specified location and the lightning strike location using the Pythagorean theorem. 
                            double rad = Math.Pow(userRad, 2);      // Computing the square of the detection threshold radius.
                            if (dist < rad)                         // Test if the userLocation and the lightningLocation distance is within the radius.
                            {

                                Task<Weather> weatherResult = weatherData.GetWeatherConditionAsync(userLat, userLon); // Fetching the weather condition at the userLocation.
                                int weatherCondition = weatherResult.Result.weather.Select(w => w.id).FirstOrDefault();  // Getting the weather condition id from the weather results.


                                if ((weatherCondition >= 800) && (weatherCondition <= 804)) // Test if the weather condition is non-precipitating. The weather condition id's between 800 and 804 correspond to sunny and cloudy. 
                                {
                                    userLocation.LastEmailTime = DateTime.Now;              // Setting the last email time in memory to now. The eager-loading prevents the LastEmailTime to be updated even after the UpdateLastEmailTime method is called.
                                    emailTime.UpdateLastEmailTime(userLocation.UserLocationId);     // Calling the method to update the LastEmailTime in the database.
                                    string userEmail = userLocation.User.Email;             // Setting the user's email.
                                    await sendEmail.SendEmailAsync(userEmail, userLocation.UserLocationName); // Calling the SendEmailAsync method to send the alert email to the user.
                                }
                            }
                        }
                    }

                }
            }
            catch (AggregateException ex)
            {
                // Ignore if error occurs. 
            }
        }
    }
}