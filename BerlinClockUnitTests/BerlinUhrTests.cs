using System;
using System.Linq;
using BerlinClock.Classes;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace BerlinClockUnitTests
{
	[TestClass]
	public class BerlinUhrTests
	{
		[TestMethod]
		public void Should_SecondDotBeLit_When_SettingTimeWithEvenSecond()
		{
			var sut = new BerlinUhr(null);
			var time = new TimeSpan(0, 0, 12);

			sut.SetTime(time);

			Assert.IsTrue(sut.SecondDot);
		}

		[TestMethod]
		public void Should_SecondDotBeLit_When_SettingTimeWithZeroInSeconds()
		{
			var sut = new BerlinUhr(null);
			var time = new TimeSpan(0, 0, 0);

			sut.SetTime(time);

			Assert.IsTrue(sut.SecondDot);
		}

		[TestMethod]
		public void Should_SecondDotBeUnlit_When_SettingTimeWithOddSecond()
		{
			var sut = new BerlinUhr(null);
			var time = new TimeSpan(0, 0, 11);

			sut.SetTime(time);

			Assert.IsFalse(sut.SecondDot);
		}

		[DataTestMethod]
		[DataRow(0)]
		[DataRow(5)]
		[DataRow(10)]
		[DataRow(15)]
		[DataRow(20)]
		[DataRow(25)]
		[DataRow(30)]
		[DataRow(35)]
		[DataRow(40)]
		[DataRow(45)]
		[DataRow(50)]
		[DataRow(55)]
		public void Should_AllOneMinutesDotsBeUnlit_When_SettingTimeWithMinuteDivisibleBy5(int minute)
		{
			var sut = new BerlinUhr(null);
			var time = new TimeSpan(0, minute, 0);

			sut.SetTime(time);

			var expected = new[] { false, false, false, false };

			CollectionAssert.AreEqual(expected, sut.OneFullMinutesRow.ToArray());
		}

		[DataTestMethod]
		[DataRow(1, new[] { true, false, false, false })]
		[DataRow(7, new[] { true, true, false, false })]
		[DataRow(13, new[] { true, true, true, false })]
		[DataRow(54, new[] { true, true, true, true })]
		public void Should_LitNextFewOneMinuteDots_When_SettingTimeWithMinuteNotDivisibleBy5(int minute, bool[] expected)
		{
			var sut = new BerlinUhr(null);
			var time = new TimeSpan(0, minute, 0);

			sut.SetTime(time);

			CollectionAssert.AreEqual(expected, sut.OneFullMinutesRow.ToArray());
		}

		[TestMethod]
		public void Should_AllFiveMinuteDotsBeUnlit_When_SettingTimeWithZeroInMinutes()
		{
			var sut = new BerlinUhr(null);
			var time = new TimeSpan(0, 0, 0);

			sut.SetTime(time);

			var expected = new[] { false, false, false, false, false, false, false, false, false, false, false };

			CollectionAssert.AreEqual(expected, sut.FiveFullMinutesRow.ToArray());
		}

		[DataTestMethod]
		[DataRow(1)]
		[DataRow(2)]
		[DataRow(3)]
		[DataRow(4)]
		public void Should_AllFiveMinuteDotsBeUnlit_When_SettingTimeWithMinuteLowerThan5(int minute)
		{
			var sut = new BerlinUhr(null);
			var time = new TimeSpan(0, minute, 0);

			sut.SetTime(time);

			var expected = new[] { false, false, false, false, false, false, false, false, false, false, false };

			CollectionAssert.AreEqual(expected, sut.FiveFullMinutesRow.ToArray());
		}

		[DataTestMethod]
		[DataRow(5, new[] { true, false, false, false, false, false, false, false, false, false, false })]
		[DataRow(27, new[] { true, true, true, true, true, false, false, false, false, false, false })]
		[DataRow(59, new[] { true, true, true, true, true, true, true, true, true, true, true })]
		public void Should_LitNextFewFiveMinuteDots_When_SettingTimeWithMinuteGreaterThan5(int minute, bool[] expected)
		{
			var sut = new BerlinUhr(null);
			var time = new TimeSpan(0, minute, 0);

			sut.SetTime(time);

			CollectionAssert.AreEqual(expected, sut.FiveFullMinutesRow.ToArray());
		}

		[DataTestMethod]
		[DataRow(0)]
		[DataRow(5)]
		[DataRow(10)]
		[DataRow(15)]
		[DataRow(20)]
		public void Should_AllOneHourDotsBeUnlit_When_SettingTimeWithHourDivisibleBy5(int hour)
		{
			var sut = new BerlinUhr(null);
			var time = new TimeSpan(hour, 0, 0);

			sut.SetTime(time);

			var expected = new[] { false, false, false, false };

			CollectionAssert.AreEqual(expected, sut.OneFullHoursRow.ToArray());
		}

		[DataTestMethod]
		[DataRow(1, new[] { true, false, false, false })]
		[DataRow(12, new[] { true, true, false, false })]
		[DataRow(18, new[] { true, true, true, false })]
		[DataRow(24, new[] { true, true, true, true })]
		public void Should_LitNextFewOneHourDots_When_SettingTimeWithHourNotDivisibleBy5(int hour, bool[] expected)
		{
			var sut = new BerlinUhr(null);
			var time = new TimeSpan(hour, 0, 0);

			sut.SetTime(time);

			CollectionAssert.AreEqual(expected, sut.OneFullHoursRow.ToArray());
		}

		[DataTestMethod]
		[DataRow(1)]
		[DataRow(2)]
		[DataRow(3)]
		[DataRow(4)]
		public void Should_AllFiveHourDotsBeUnlit_When_SettingTimeWithHourLowerThan5(int hour)
		{
			var sut = new BerlinUhr(null);
			var time = new TimeSpan(hour, 0, 0);

			sut.SetTime(time);

			var expected = new[] { false, false, false, false };

			CollectionAssert.AreEqual(expected, sut.FiveFullHoursRow.ToArray());
		}

		[DataTestMethod]
		[DataRow(6, new[] { true, false, false, false })]
		[DataRow(12, new[] { true, true, false, false })]
		[DataRow(19, new[] { true, true, true, false })]
		[DataRow(21, new[] { true, true, true, true })]
		public void Should_LitNextFewFiveHourDots_When_SettingTimeWithHourGreaterThan5(int hour, bool[] expected)
		{
			var sut = new BerlinUhr(null);
			var time = new TimeSpan(hour, 0, 0);

			sut.SetTime(time);

			CollectionAssert.AreEqual(expected, sut.FiveFullHoursRow.ToArray());
		}

		[TestMethod]
		public void Should_LitOnlySecondDot_When_SettingTimeAsMidnight0000()
		{
			var sut = new BerlinUhr(null);
			var time = new TimeSpan(0, 0, 0);

			sut.SetTime(time);

			var expectedOneFullMinutes = new[] { false, false, false, false };
			var expectedFiveFullMinutes = new[] { false, false, false, false, false, false, false, false, false, false, false };
			var expectedOneFullHours = new[] { false, false, false, false };
			var expectedFiveFullHours = new[] { false, false, false, false };


			Assert.IsTrue(sut.SecondDot);
			CollectionAssert.AreEqual(expectedOneFullMinutes, sut.OneFullMinutesRow.ToArray());
			CollectionAssert.AreEqual(expectedFiveFullMinutes, sut.FiveFullMinutesRow.ToArray());
			CollectionAssert.AreEqual(expectedOneFullHours, sut.OneFullHoursRow.ToArray());
			CollectionAssert.AreEqual(expectedFiveFullHours, sut.FiveFullHoursRow.ToArray());
		}

		[TestMethod]
		public void Should_LitAllHourAndSecondAndNoMinuteDots_When_SettingTimeAsMidnight2400()
		{
			var sut = new BerlinUhr(null);
			var time = new TimeSpan(24, 0, 0);

			sut.SetTime(time);

			var expectedOneFullMinutes = new[] { false, false, false, false };
			var expectedFiveFullMinutes = new[] { false, false, false, false, false, false, false, false, false, false, false };
			var expectedOneFullHours = new[] { true, true, true, true };
			var expectedFiveFullHours = new[] { true, true, true, true };

			Assert.IsTrue(sut.SecondDot);
			CollectionAssert.AreEqual(expectedOneFullMinutes, sut.OneFullMinutesRow.ToArray());
			CollectionAssert.AreEqual(expectedFiveFullMinutes, sut.FiveFullMinutesRow.ToArray());
			CollectionAssert.AreEqual(expectedOneFullHours, sut.OneFullHoursRow.ToArray());
			CollectionAssert.AreEqual(expectedFiveFullHours, sut.FiveFullHoursRow.ToArray());
		}

		[TestMethod]
		public void Should_LitAllDotsExceptLastOnFullHourAndSecondDot_When_SettingTimeAsMidnight2359()
		{
			var sut = new BerlinUhr(null);
			var time = new TimeSpan(23, 59, 59);

			sut.SetTime(time);

			var expectedOneFullMinutes = new[] { true, true, true, true };
			var expectedFiveFullMinutes = new[] { true, true, true, true, true, true, true, true, true, true, true };
			var expectedOneFullHours = new[] { true, true, true, false };
			var expectedFiveFullHours = new[] { true, true, true, true };

			Assert.IsFalse(sut.SecondDot);
			CollectionAssert.AreEqual(expectedOneFullMinutes, sut.OneFullMinutesRow.ToArray());
			CollectionAssert.AreEqual(expectedFiveFullMinutes, sut.FiveFullMinutesRow.ToArray());
			CollectionAssert.AreEqual(expectedOneFullHours, sut.OneFullHoursRow.ToArray());
			CollectionAssert.AreEqual(expectedFiveFullHours, sut.FiveFullHoursRow.ToArray());
		}

		[TestMethod]
		public void Should_CorrectlySetTime_When_SettingExampleTime()
		{
			var sut = new BerlinUhr(null);
			var time = new TimeSpan(13, 17, 01);

			sut.SetTime(time);

			var expectedOneFullMinutes = new[] { true, true, false, false };
			var expectedFiveFullMinutes = new[] { true, true, true, false, false, false, false, false, false, false, false };
			var expectedOneFullHours = new[] { true, true, true, false };
			var expectedFiveFullHours = new[] { true, true, false, false };

			Assert.IsFalse(sut.SecondDot);
			CollectionAssert.AreEqual(expectedOneFullMinutes, sut.OneFullMinutesRow.ToArray());
			CollectionAssert.AreEqual(expectedFiveFullMinutes, sut.FiveFullMinutesRow.ToArray());
			CollectionAssert.AreEqual(expectedOneFullHours, sut.OneFullHoursRow.ToArray());
			CollectionAssert.AreEqual(expectedFiveFullHours, sut.FiveFullHoursRow.ToArray());
		}

		[TestMethod]
		public void Should_CorrectlySetTime_When_SettingTimeFromLitToUnlit()
		{
			var sut = new BerlinUhr(null);
			var litTime = new TimeSpan(23, 59, 59);
			var unlitTime = new TimeSpan(0, 0, 0);

			sut.SetTime(litTime);
			sut.SetTime(unlitTime);

			var expectedOneFullMinutes = new[] { false, false, false, false };
			var expectedFiveFullMinutes = new[] { false, false, false, false, false, false, false, false, false, false, false };
			var expectedOneFullHours = new[] { false, false, false, false };
			var expectedFiveFullHours = new[] { false, false, false, false };

			Assert.IsTrue(sut.SecondDot);
			CollectionAssert.AreEqual(expectedOneFullMinutes, sut.OneFullMinutesRow.ToArray());
			CollectionAssert.AreEqual(expectedFiveFullMinutes, sut.FiveFullMinutesRow.ToArray());
			CollectionAssert.AreEqual(expectedOneFullHours, sut.OneFullHoursRow.ToArray());
			CollectionAssert.AreEqual(expectedFiveFullHours, sut.FiveFullHoursRow.ToArray());
		}

		[TestMethod]
		public void Should_CorrectlySetTime_When_SettingTimeFromUnlitToLit()
		{
			var sut = new BerlinUhr(null);
			var litTime = new TimeSpan(23, 59, 59);
			var unlitTime = new TimeSpan(0, 0, 0);

			sut.SetTime(unlitTime);
			sut.SetTime(litTime);

			var expectedOneFullMinutes = new[] { true, true, true, true };
			var expectedFiveFullMinutes = new[] { true, true, true, true, true, true, true, true, true, true, true };
			var expectedOneFullHours = new[] { true, true, true, false };
			var expectedFiveFullHours = new[] { true, true, true, true };

			Assert.IsFalse(sut.SecondDot);
			CollectionAssert.AreEqual(expectedOneFullMinutes, sut.OneFullMinutesRow.ToArray());
			CollectionAssert.AreEqual(expectedFiveFullMinutes, sut.FiveFullMinutesRow.ToArray());
			CollectionAssert.AreEqual(expectedOneFullHours, sut.OneFullHoursRow.ToArray());
			CollectionAssert.AreEqual(expectedFiveFullHours, sut.FiveFullHoursRow.ToArray());
		}

		[TestMethod]
		public void Should_PrintTime_Using_SelectedStrategy()
		{
			var expected = "test";
			var strategyMock = new Mock<IBerlinUhrPrintStrategy>();
			var sut = new BerlinUhr(strategyMock.Object);

			strategyMock
				.Setup(strategy => strategy.Print(sut))
				.Returns(expected);

			var actual = sut.Print();

			Assert.AreEqual(expected, actual);
		}
	}
}
