﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleAppAccuWeather.Models
{
    internal class Temperature
    {
        public Metric Metric { get; set; }
        public Imperial Imperial { get; set; }
        public Minimum Minimum { get; set; }
        public Maximum Maximum { get; set; }
    }

    internal class Minimum
    {
        public double Value { get; set; }
        public string Unit { get; set; }
        public int UnitType { get; set; }
    }

    internal class Maximum
    {
        public double Value { get; set; }
        public string Unit { get; set; }
        public int UnitType { get; set; }
    }
}