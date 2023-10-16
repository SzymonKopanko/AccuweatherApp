using System;
using System.Threading.Tasks;
using ConsoleAppAccuWeather.Services;
using ConsoleAppAccuWeather.Models;

namespace ConsoleAppAccuWeather
{
    class Program
    {
        static async Task Main(string[] args)
        {
            AccuWeatherService accuWeatherService = new AccuWeatherService("fKhqcJlCz86IhcTZ6xpLCVJcnyEMATai", "pl");
            Console.WriteLine("Enter a city name:");
            string cityToSearch = Console.ReadLine();

            try
            {
                City[] cities = await accuWeatherService.GetLocations(cityToSearch);

                if (cities.Length == 0)
                {
                    Console.WriteLine("No matching cities found.");
                }
                else
                {
                    Console.WriteLine("Matching cities:");
                    for (int i = 0; i < cities.Length; i++)
                    {
                        Console.WriteLine($"{i + 1}. {cities[i].LocalizedName}");
                    }

                    Console.WriteLine("Select a city by entering its number:");
                    if (int.TryParse(Console.ReadLine(), out int selectedCityIndex) && selectedCityIndex > 0 && selectedCityIndex <= cities.Length)
                    {
                        City selectedCity = cities[selectedCityIndex - 1];
                        Weather weather = await accuWeatherService.GetCurrentConditions(selectedCity.Key);

                        Console.WriteLine($"Weather conditions for {selectedCity.LocalizedName}:");
                        Console.WriteLine($"Local Observation Date/Time: {weather.LocalObservationDateTime}");
                        Console.WriteLine($"Temperature: {weather.Temperature.Metric.Value} {weather.Temperature.Metric.Unit}");
                        Console.WriteLine($"Weather Text: {weather.WeatherText}");
                        Console.WriteLine($"Is Daytime: {weather.IsDayTime}");

                        // Dodane informacje o prognozach pogody
                        Console.WriteLine("\nWeather Forecasts:");

                        // Prognoza dzienna
                        Forecast[] dailyForecasts = await accuWeatherService.GetDailyForecast(selectedCity.Key);
                        Console.WriteLine("5-day Daily Forecast:");
                        foreach (var forecast in dailyForecasts)
                        {
                            Console.WriteLine($"Date: {forecast.Date}, Max Temp: {forecast.Temperature.Maximum.Value} {forecast.Temperature.Maximum.Unit}, Min Temp: {forecast.Temperature.Minimum.Value} {forecast.Temperature.Minimum.Unit}");
                        }

                        // Prognoza godzinowa
                        Forecast[] hourlyForecasts = await accuWeatherService.GetHourlyForecast(selectedCity.Key);
                        Console.WriteLine("12-hour Hourly Forecast:");
                        foreach (var forecast in hourlyForecasts)
                        {
                            Console.WriteLine($"Date: {forecast.Date}, Temp: {forecast.Temperature.Value} {forecast.Temperature.Unit}, Weather Text: {forecast.IconPhrase}");
                        }

                        // Prognoza dzienna z tekstem
                        ForecastWithText[] dailyForecastsText = await accuWeatherService.GetDailyForecastWithText(selectedCity.Key);
                        Console.WriteLine("5-day Daily Forecast with Text:");
                        foreach (var forecast in dailyForecastsText)
                        {
                            Console.WriteLine($"Date: {forecast.Date}, Max Temp: {forecast.Temperature.Maximum.Value} {forecast.Temperature.Maximum.Unit}, Min Temp: {forecast.Temperature.Minimum.Value} {forecast.Temperature.Minimum.Unit}, Weather Text: {forecast.IconPhrase}");
                        }

                        // Prognoza godzinowa z tekstem
                        ForecastWithText[] hourlyForecastsText = await accuWeatherService.GetHourlyForecastWithText(selectedCity.Key);
                        Console.WriteLine("12-hour Hourly Forecast with Text:");
                        foreach (var forecast in hourlyForecastsText)
                        {
                            Console.WriteLine($"Date: {forecast.Date}, Temp: {forecast.Temperature.Value} {forecast.Temperature.Unit}, Weather Text: {forecast.IconPhrase}");
                        }
                    }
                    else
                    {
                        Console.WriteLine("Invalid input. Please enter a valid city number.");
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("An error occurred: " + ex.Message);
            }

            Console.WriteLine("Press any key to exit...");
            Console.ReadKey(); // Program pozostanie uruchomiony do momentu naciśnięcia dowolnego klawisza.
        }
    }
}
