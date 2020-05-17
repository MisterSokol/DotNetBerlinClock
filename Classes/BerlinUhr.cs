using System;
using System.Collections.Generic;

namespace BerlinClock.Classes
{
	public class BerlinUhr : IBerlinUhr
	{
		private readonly IBerlinUhrPrintStrategy printStrategy;

		public BerlinUhr(IBerlinUhrPrintStrategy printStrategy)
		{
			this.printStrategy = printStrategy;
		}

		public bool SecondDot { get; private set; }

		public IEnumerable<bool> FiveFullHoursRow => throw new NotImplementedException();

		public IEnumerable<bool> OneFullHoursRow => throw new NotImplementedException();

		public IEnumerable<bool> FiveFullMinutesRow => throw new NotImplementedException();

		public IEnumerable<bool> OneFullMinutesRow => throw new NotImplementedException();

		public string Print()
		{
			return this.printStrategy.Print(this);
		}

		public void SetTime(TimeSpan time)
		{
			this.SetSecondDot(time);
		}

		private void SetSecondDot(TimeSpan time)
		{
			this.SecondDot = time.Seconds % 2 == 0;
		}
	}
}
