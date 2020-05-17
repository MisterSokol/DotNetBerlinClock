using System;
using System.Collections;
using System.Collections.Generic;

namespace BerlinClock.Classes
{
	public interface IBerlinUhr
	{
		bool SecondDot { get; }
		IEnumerable<bool> FiveFullHoursRow { get; }
		IEnumerable<bool> OneFullHoursRow { get; }
		IEnumerable<bool> FiveFullMinutesRow { get; }
		IEnumerable<bool> OneFullMinutesRow { get; }

		void SetTime(TimeSpan time);
		string Print();
	}
}
