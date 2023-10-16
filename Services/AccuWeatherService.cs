using Newtonsoft.Json;
using ConsoleAppAccuWeather.Models;
using System;
using System.Net.Http;
using System.Threading.Tasks;
using System.Linq;

namespace ConsoleAppAccuWeather.Services
{
    internal class AccuWeatherService
    {
        private const string base_url = "http://dataservice.accuweather.com";
        private const string autocomplete_endpoint = "locations/v1/cities/autocomplete?apikey={0}&q={1}&language{2}";
        private const string current_conditions_endpoint = "currentconditions/v1/{0}?apikey={1}&language{2}";

        private const string daily_forecast_endpoint = "forecasts/v1/daily/5day/{0}?apikey={1}&language={2}&metric=true";
        private const string hourly_forecast_endpoint = "forecasts/v1/hourly/12hour/{0]?apikey={1}&language={2}&metric=true";
        private const string daily_forecast_with_text_endpoint = "forecasts/v1/daily/5day/{0}?apikey={1}&language={2}&details=true";
        private const string hourly_forecast_with_text_endpoint = "forecasts/v1/hourly/12hour/{0}?apikey={1}&language={2}&details=true";

        private string api_key;
        private string language;

        public AccuWeatherService(string apiKey, string defaultLanguage)
        {
            api_key = apiKey;
            language = defaultLanguage;
        }

        public AccuWeatherService(){}

        public async Task<City[]> GetLocations(string locationName)
        {
            string uri = base_url + "/" + string.Format(autocomplete_endpoint, api_key, locationName, language);
            using (HttpClient client = new HttpClient())
            {
                var response = await client.GetAsync(uri);
                string json = await response.Content.ReadAsStringAsync();
                Console.WriteLine(json);
                City[] cities = JsonConvert.DeserializeObject<City[]>(json);
                return cities;
            }
        }

        public async Task<Weather> GetCurrentConditions(string cityKey)
        {
            string uri = base_url + "/" + string.Format(current_conditions_endpoint, cityKey, api_key, language);
            using (HttpClient client = new HttpClient())
            {
                var response = await client.GetAsync(uri);
                string json = await response.Content.ReadAsStringAsync();
                Console.WriteLine(json);
                Weather[] weathers = JsonConvert.DeserializeObject<Weather[]>(json);
                return weathers.FirstOrDefault();
            }
        }

        public async Task<Forecast[]> GetDailyForecast(string cityKey)
        {
            string uri = base_url + "/" + string.Format(daily_forecast_endpoint, cityKey, api_key, language);
            using (HttpClient client = new HttpClient())
            {
                var response = await client.GetAsync(uri);
                string json = await response.Content.ReadAsStringAsync();
                Console.WriteLine(json);
                Forecast[] forecasts = JsonConvert.DeserializeObject<Forecast[]>(json);
                return forecasts;
            }
        }


        public async Task<Forecast[]> GetHourlyForecast(string cityKey)
        {
            string uri = base_url + "/" + string.Format(hourly_forecast_endpoint, cityKey, api_key, language);
            using (HttpClient client = new HttpClient())
            {
                var response = await client.GetAsync(uri);
                string json = await response.Content.ReadAsStringAsync();
                Console.WriteLine(json);
                Forecast[] forecasts = JsonConvert.DeserializeObject<Forecast[]>(json);
                return forecasts;
            }
        }

        public async Task<ForecastWithText[]> GetDailyForecastWithText(string cityKey)
        {
            string uri = base_url + "/" + string.Format(daily_forecast_with_text_endpoint, cityKey, api_key, language);
            using (HttpClient client = new HttpClient())
            {
                var response = await client.GetAsync(uri);
                string json = await response.Content.ReadAsStringAsync();
                Console.WriteLine(json);
                ForecastWithText[] forecasts = JsonConvert.DeserializeObject<ForecastWithText[]>(json);
                return forecasts;
            }
        }

        public async Task<ForecastWithText[]> GetHourlyForecastWithText(string cityKey)
        {
            string uri = base_url + "/" + string.Format(hourly_forecast_with_text_endpoint, cityKey, api_key, language);
            using (HttpClient client = new HttpClient())
            {
                var response = await client.GetAsync(uri);
                string json = await response.Content.ReadAsStringAsync();
                Console.WriteLine(json);
                ForecastWithText[] forecasts = JsonConvert.DeserializeObject<ForecastWithText[]>(json);
                return forecasts;
            }
        }
    }
}
