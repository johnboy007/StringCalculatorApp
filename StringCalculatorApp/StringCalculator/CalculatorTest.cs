using System;
using NUnit.Framework;
using StringCalculatorApp;

namespace StringCalculatorTest
{
    [TestFixture]
    public class CalculatorTest
    {
        //http://osherove.com/tdd-kata-1/

        private const string one = "1";
        private const string two = "2";
        private const string three = "3";
        private const string minusThree = "-3";
        private const string oneThousandOneHundred = "1100";
        private Calculator calc;

        [SetUp]
        public void SetUp()
        {
            calc = new Calculator();
            Assert.IsNotNull(calc);
        }

        [Test]
        public void CanAdd1or2Numbers()
        {
            Assert.AreEqual(calc.Add($"{one},{two}"), Convert.ToInt32(one) + Convert.ToInt32(two));
        }

        [Test]
        public void CanGet0WhenNullString()
        {
            Assert.AreEqual(calc.Add(null), 0);
        }

        [Test]
        public void CanAddWithNewLinesAndCommas()
        {
            Assert.AreEqual(calc.Add($"{one}\n{two},{three}"), Convert.ToInt32(one) + Convert.ToInt32(two) + Convert.ToInt32(three));
        }

        [Test]
        public void CanSupportDifferentDelimiters()
        {
            Assert.AreEqual(calc.Add($"//;\n{one};{two}"), Convert.ToInt32(one) + Convert.ToInt32(two));
        }

        [Test]
        public void CallingAddWithNegativeWillThrowException()
        {
            try
            {
                calc.Add($"{one};{two},{minusThree}");
                Assert.Fail();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                Assert.Pass();
            }
        }

        [Test]
        public void NumbersBiggerThan1000Ignored()
        {
            Assert.AreEqual(calc.Add($"{one};{two},{oneThousandOneHundred}"), Convert.ToInt32(one) + Convert.ToInt32(two));
        }

        [Test]
        public void DelimitersCanBeAnyLength()
        {
            Assert.AreEqual(calc.Add($"//[***]\n{one}***{two}***{three}"), Convert.ToInt32(one) + Convert.ToInt32(two) + Convert.ToInt32(three));
        }

        [Test]
        public void HandleMulipleDelimiters()
        {
            Assert.AreEqual(calc.Add($"//[*][%]\n{one}*{two}%{three}"), Convert.ToInt32(one) + Convert.ToInt32(two) + Convert.ToInt32(three));
        }

        [Test]
        public void HandleMulipleDelimitersWithLengthLongerThan1Char()
        {
            Assert.AreEqual(calc.Add($"//[**][%%]\n{one}**{two}%%{three}"), Convert.ToInt32(one) + Convert.ToInt32(two) + Convert.ToInt32(three));
        }
    }
}