﻿using System;
using System.Collections.Generic;
using System.Linq;

namespace BerlinClock.Classes
{
	public class BerlinUhr : IBerlinUhr
	{
		private readonly IBerlinUhrPrintStrategy printStrategy;

		private bool[] oneFullMinutesRow = new bool[4];
		private bool[] fiveFullMinutesRow = new bool[11];

		public BerlinUhr(IBerlinUhrPrintStrategy printStrategy)
		{
			this.printStrategy = printStrategy;
		}

		public bool SecondDot { get; private set; }

		public IEnumerable<bool> FiveFullHoursRow => throw new NotImplementedException();

		public IEnumerable<bool> OneFullHoursRow => throw new NotImplementedException();

		public IEnumerable<bool> FiveFullMinutesRow => this.fiveFullMinutesRow;

		public IEnumerable<bool> OneFullMinutesRow => this.oneFullMinutesRow;

		public string Print()
		{
			return this.printStrategy.Print(this);
		}

		public void SetTime(TimeSpan time)
		{
			this.SetSecondDot(time);
			this.SetOneFullMinutesRow(time);
			this.SetFiveFullMinutesRow(time);
		}

		private void SetFiveFullMinutesRow(TimeSpan time)
		{
			int litSquares = time.Minutes / 5;

			this.LitSquares(this.fiveFullMinutesRow, litSquares);
		}

		private void SetOneFullMinutesRow(TimeSpan time)
		{
			var litSquares = time.Minutes % 5;

			this.LitSquares(this.oneFullMinutesRow, litSquares);
		}

		private void SetSecondDot(TimeSpan time)
		{
			this.SecondDot = time.Seconds % 2 == 0;
		}

		private void LitSquares(bool[] rows, int litSquares)
		{
			var size = rows.Length;

			for (int i = 0; i < size; i++)
			{
				rows[i] = i < litSquares;
			}
		}
	}
}
