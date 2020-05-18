using BerlinClock.Classes;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace BerlinClockUnitTests
{
	[TestClass]
	public class BerlinUhrPrintStrategyTests
	{
		private Mock<IBerlinUhr> berlinUhrMock;

		BerlinUhrPrintStrategy sut;
		
		[TestInitialize]
		public void TestInitialize()
		{
			this.sut = new BerlinUhrPrintStrategy();

			this.berlinUhrMock = new Mock<IBerlinUhr>();

			this.berlinUhrMock
				.Setup(uhr => uhr.OneFullMinutesRow)
				.Returns(new bool[4]);
			this.berlinUhrMock
				.Setup(uhr => uhr.FiveFullMinutesRow)
				.Returns(new bool[11]);
			this.berlinUhrMock
				.Setup(uhr => uhr.OneFullHoursRow)
				.Returns(new bool[4]);
			this.berlinUhrMock
				.Setup(uhr => uhr.FiveFullHoursRow)
				.Returns(new bool[4]);
		}

		[TestMethod]
		public void Should_AllDotsBeOff_When_EveryDotValueIsFalse()
		{
			var expected = 
				"O\r\n"+
				"OOOO\r\n" +
				"OOOO\r\n" +
				"OOOOOOOOOOO\r\n" +
				"OOOO";

			var actual = sut.Print(this.berlinUhrMock.Object);

			Assert.AreEqual(expected, actual);
		}

		[TestMethod]
		public void Should_SecondDotBeY_When_SecondDotValueIsTrue()
		{
			var expected =
				"Y\r\n" +
				"OOOO\r\n" +
				"OOOO\r\n" +
				"OOOOOOOOOOO\r\n" +
				"OOOO";

			this.berlinUhrMock
				.Setup(uhr => uhr.SecondDot)
				.Returns(true);

			var actual = sut.Print(this.berlinUhrMock.Object);

			Assert.AreEqual(expected, actual);
		}

		[TestMethod]
		public void Should_AllFullFiveHourDotsBeR_When_AllFullFiveHourValuesAreTrue()
		{
			var expected =
				"O\r\n" +
				"RRRR\r\n" +
				"OOOO\r\n" +
				"OOOOOOOOOOO\r\n" +
				"OOOO";

			this.berlinUhrMock
				.Setup(uhr => uhr.FiveFullHoursRow)
				.Returns(new[] { true, true, true, true});

			var actual = sut.Print(this.berlinUhrMock.Object);

			Assert.AreEqual(expected, actual);
		}

		[TestMethod]
		public void Should_AllFullOneHourDotsBeR_When_AllFullOneHourValuesAreTrue()
		{
			var expected =
				"O\r\n" +
				"OOOO\r\n" +
				"RRRR\r\n" +
				"OOOOOOOOOOO\r\n" +
				"OOOO";

			this.berlinUhrMock
				.Setup(uhr => uhr.OneFullHoursRow)
				.Returns(new[] { true, true, true, true });

			var actual = sut.Print(this.berlinUhrMock.Object);

			Assert.AreEqual(expected, actual);
		}

		[TestMethod]
		public void Should_AllQuarterMinutesBeRAndRestY_When_AllFullFiveMinutesaluesAreTrue()
		{
			var expected =
				"O\r\n" +
				"OOOO\r\n" +
				"OOOO\r\n" +
				"YYRYYRYYRYY\r\n" +
				"OOOO";

			this.berlinUhrMock
				.Setup(uhr => uhr.FiveFullMinutesRow)
				.Returns(new[] { true, true, true, true, true, true, true, true, true, true, true });

			var actual = sut.Print(this.berlinUhrMock.Object);

			Assert.AreEqual(expected, actual);
		}

		[TestMethod]
		public void Should_AllFullOneMinutesDotsBeY_When_AllFullOneMinutesValuesAreTrue()
		{
			var expected =
				"O\r\n" +
				"OOOO\r\n" +
				"OOOO\r\n" +
				"OOOOOOOOOOO\r\n" +
				"YYYY";

			this.berlinUhrMock
				.Setup(uhr => uhr.OneFullMinutesRow)
				.Returns(new[] { true, true, true, true });

			var actual = sut.Print(this.berlinUhrMock.Object);

			Assert.AreEqual(expected, actual);
		}
	}
}
