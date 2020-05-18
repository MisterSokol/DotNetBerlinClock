﻿using BerlinClock.Classes;
using System;
using System.Linq;

namespace BerlinClock
{
    public class TimeConverter : ITimeConverter
    {
        public string convertTime(string aTime)
        {
            var time = this.GetTime(aTime);
            var berlinUhr = new BerlinUhr(new BerlinUhrPrintStrategy());

            berlinUhr.SetTime(time);

            return berlinUhr.Print();
        }

        private TimeSpan GetTime(string aTime)
        {
            var timeParts = aTime
                .Split(':')
                .Select(tpart => int.Parse(tpart))
                .ToArray();

            return new TimeSpan(timeParts[0], timeParts[1], timeParts[2]);
        }
    }
}
