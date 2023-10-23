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
                        Console.WriteLine();

                        DailyForecast[] dailyForecasts = await accuWeatherService.GetDailyForecast(selectedCity.Key);
                        if (dailyForecasts != null && dailyForecasts.Length > 0)
                        {
                            Console.WriteLine("Daily Forecasts:");
                            foreach (var forecast in dailyForecasts)
                            {
                                Console.WriteLine($"Date: {forecast.Date.ToShortDateString()}");
                                Console.WriteLine($"Temperature: {forecast.Temperature.Maximum.Value} {forecast.Temperature.Maximum.Unit} (Max) / {forecast.Temperature.Minimum.Value} {forecast.Temperature.Minimum.Unit} (Min)");
                                Console.WriteLine($"Day Weather: {forecast.Day.IconPhrase}");
                                Console.WriteLine($"Night Weather: {forecast.Night.IconPhrase}");
                                Console.WriteLine();
                            }
                        }
                        HourlyForecast[] hourlyForecasts = await accuWeatherService.GetHourlyForecast(selectedCity.Key);
                        if (hourlyForecasts != null && hourlyForecasts.Length > 0)
                        {
                            Console.WriteLine("Hourly Forecasts:");
                            foreach (var forecast in hourlyForecasts)
                            {
                                Console.WriteLine($"DateTime: {forecast.DateTime}");
                                Console.WriteLine($"Temperature: {forecast.Temperature.Value} {forecast.Temperature.Unit} ");
                                Console.WriteLine($"Weather: {forecast.IconPhrase}");
                                Console.WriteLine();
                            }
                        }
                        Weather[] historicalConditions = await accuWeatherService.GetHistoricalConditions(selectedCity.Key);
                        Console.WriteLine("Historical Conditions:");
                        foreach (var condition in historicalConditions)
                        {
                            Console.WriteLine($"DateTime: {condition.LocalObservationDateTime}");
                            Console.WriteLine($"Temperature: {condition.Temperature.Metric.Value} {condition.Temperature.Metric.Unit}");
                            Console.WriteLine($"IconPhrase: {condition.WeatherText}");
                            Console.WriteLine();
                        }
                        AlarmData[] alarms = await accuWeatherService.GetAlarms(selectedCity.Key);
                        if (alarms != null && alarms.Length > 0)
                        {
                            Console.WriteLine("Alarms for the selected city:");
                            foreach (var alarm in alarms)
                            {
                                Console.WriteLine($"Date: {alarm.Date}");
                                foreach (var a in alarm.Alarms)
                                {
                                    Console.WriteLine($"Alarm Type: {a.AlarmType}");
                                    Console.WriteLine($"Value: {a.Value.Metric.Value} {a.Value.Metric.Unit}");
                                    Console.WriteLine($"Day Weather: {a.Day.IconPhrase}");
                                    Console.WriteLine($"Night Weather: {a.Night.IconPhrase}");
                                }
                                Console.WriteLine();
                            }
                        }
                        else
                        {
                            Console.WriteLine("No alarms for the selected city.");
                        }

                        Index[] indexes = await accuWeatherService.GetIndices(selectedCity.Key);
                        if (indexes != null && indexes.Length > 0)
                        {
                            Console.WriteLine("Weather Indices for the selected city:");
                            foreach (var index in indexes)
                            {
                                Console.WriteLine($"Index Name: {index.Name}");
                                Console.WriteLine($"Category: {index.Category}");
                                Console.WriteLine();
                            }
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
            Console.ReadKey(); 
        }
    }
}