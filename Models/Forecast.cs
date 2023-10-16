using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleAppAccuWeather.Models
{
    internal class Forecast
    {
        public DateTime Date { get; set; }
        public int EpochDate { get; set; }
        public Temperature Temperature { get; set; }
        public int DayIcon { get; set; }
        public string DayIconPhrase { get; set; }
        public int NightIcon { get; set; }
        public string NightIconPhrase { get; set; }
    }
}
