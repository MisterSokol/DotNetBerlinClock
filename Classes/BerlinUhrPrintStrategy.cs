using System;
using System.Linq;
using System.Text;

namespace BerlinClock.Classes
{
	public class BerlinUhrPrintStrategy : IBerlinUhrPrintStrategy
	{
		private const string offValue = "O";
		private const string hourValue = "R";
		private const string minuteValue = "Y";
		private const string quarterValue = "R";
		private const string secondValue = "Y";

		public string Print(IBerlinUhr berlinUhr)
		{
			var stringBuilder = new StringBuilder();

			stringBuilder.AppendLine(this.GetSecondDotString(berlinUhr));
			stringBuilder.AppendLine(this.GetFiveFullHoursRowString(berlinUhr));
			stringBuilder.AppendLine(this.GetOneFullHoursRowString(berlinUhr)); 
			stringBuilder.AppendLine(this.GetFiveFullMinutesRowString(berlinUhr)); 
			stringBuilder.Append(this.GetOneFullMinutesRowString(berlinUhr));

			return stringBuilder.ToString();
		}

		private string GetOneFullMinutesRowString(IBerlinUhr berlinUhr)
		{
			var stringBuilder = new StringBuilder();

			foreach (var fullOne in berlinUhr.OneFullMinutesRow)
			{
				stringBuilder.Append(fullOne ? minuteValue : offValue);
			}

			return stringBuilder.ToString();
		}

		private string GetFiveFullMinutesRowString(IBerlinUhr berlinUhr)
		{
			var stringBuilder = new StringBuilder();
			var fullFiveMinutes = berlinUhr.FiveFullMinutesRow.ToList();

			for (int i = 1; i <= fullFiveMinutes.Count; i++)
			{
				var isQuarter = i % 3 == 0;
				var onValue = isQuarter ? quarterValue : minuteValue;
				stringBuilder.Append(fullFiveMinutes[i-1] ? onValue : offValue);
			}

			return stringBuilder.ToString();
		}

		private string GetOneFullHoursRowString(IBerlinUhr berlinUhr)
		{
			var stringBuilder = new StringBuilder();

			foreach (var fullOne in berlinUhr.OneFullHoursRow)
			{
				stringBuilder.Append(fullOne ? hourValue : offValue);
			}

			return stringBuilder.ToString();
		}

		private string GetFiveFullHoursRowString(IBerlinUhr berlinUhr)
		{
			var stringBuilder = new StringBuilder();

			foreach (var fullFive in berlinUhr.FiveFullHoursRow)
			{
				stringBuilder.Append(fullFive ? hourValue : offValue);
			}

			return stringBuilder.ToString();
		}

		private string GetSecondDotString(IBerlinUhr berlinUhr)
		{
			return berlinUhr.SecondDot ? secondValue : offValue;
		}
	}
}
