using System;
using System.Collections.Generic;

namespace BerlinClock.Classes
{
	public class BerlinUhr : IBerlinUhr
	{
		public BerlinUhr(IBerlinUhrPrintStrategy printStrategy)
		{
		}

		public bool SecondDot => throw new NotImplementedException();

		public IEnumerable<bool> FiveFullHoursRow => throw new NotImplementedException();

		public IEnumerable<bool> OneFullHoursRow => throw new NotImplementedException();

		public IEnumerable<bool> FiveFullMinutesRow => throw new NotImplementedException();

		public IEnumerable<bool> OneFullMinutesRow => throw new NotImplementedException();

		public string Print()
		{
			throw new NotImplementedException();
		}

		public void SetTime(TimeSpan time)
		{
			throw new NotImplementedException();
		}
	}
}
