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
        private const string hourly_forecast_endpoint = "forecasts/v1/hourly/12hour/{0}?apikey={1}&language={2}&metric=true";
        private const string historical_conditions_endpoint = "currentconditions/v1/{0}/historical?apikey={1}&language{2}";
        private const string alarms_endpoint = "alarms/v1/1day/{0}?apikey={1}&language{2}";
        private const string indices_endpoint = "indices/v1/daily/1day/{0}?apikey={1}&language{2}";

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
                Weather[] weathers = JsonConvert.DeserializeObject<Weather[]>(json);
                return weathers.FirstOrDefault();
            }
        }

        public async Task<DailyForecast[]> GetDailyForecast(string cityKey)
        {
            string uri = base_url + "/" + string.Format(daily_forecast_endpoint, cityKey, api_key, language);
            using (HttpClient client = new HttpClient())
            {
                var response = await client.GetAsync(uri);
                string json = await response.Content.ReadAsStringAsync();
                WeatherData data = JsonConvert.DeserializeObject<WeatherData>(json);
                return data.DailyForecasts.ToArray();
            }
        }

        public async Task<HourlyForecast[]> GetHourlyForecast(string cityKey)
        {
            string uri = base_url + "/" + string.Format(hourly_forecast_endpoint, cityKey, api_key, language);
            using (HttpClient client = new HttpClient())
            {
                var response = await client.GetAsync(uri);
                string json = await response.Content.ReadAsStringAsync();
                HourlyForecast[] data = JsonConvert.DeserializeObject<HourlyForecast[]>(json).ToArray();
                return data;
            }
        }

        public async Task<Weather[]> GetHistoricalConditions(string cityKey)
        {
            string uri = base_url + "/" + string.Format(historical_conditions_endpoint, cityKey, api_key, language);
            using (HttpClient client = new HttpClient())
            {
                var response = await client.GetAsync(uri);
                string json = await response.Content.ReadAsStringAsync();
                Weather[] weathers = JsonConvert.DeserializeObject<Weather[]>(json).ToArray();
                return weathers;
            }
        }

        public async Task<AlarmData[]> GetAlarms(string cityKey)
        {
            string uri = base_url + "/" + string.Format(alarms_endpoint, cityKey, api_key, language);
            using (HttpClient client = new HttpClient())
            {
                var response = await client.GetAsync(uri);
                var json = await response.Content.ReadAsStringAsync();
                AlarmData[] alarms = JsonConvert.DeserializeObject<AlarmData[]>(json);
                return alarms;

            }
        }
        public async Task<Index[]> GetIndices(string cityKey)
        {
            string uri = base_url + "/" + string.Format(indices_endpoint, cityKey, api_key, language);
            using (HttpClient client = new HttpClient())
            {
                var response = await client.GetAsync(uri);
                var json = await response.Content.ReadAsStringAsync();
                Index[] indexes = JsonConvert.DeserializeObject<Index[]>(json);
                return indexes;
            }
        }
    }
}
