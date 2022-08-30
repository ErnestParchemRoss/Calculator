using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Calculator.Tests
{
    public class StringCalculatorTests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void ShouldReturnZeroForEmptyString()
        {
            StringCalculator cs = new StringCalculator();

            Assert.That(cs.Add(""), Is.EqualTo(0));
        }

        [TestCase("5", 5)]
        [TestCase("32", 32)]
        [TestCase("//x\n555", 555)]
        [TestCase("//[x][aaadf]\n555", 555)]
        public void ShouldAddSingleNumber(string number, int expectedResult)
        {
            StringCalculator cs = new StringCalculator();

            Assert.That(cs.Add(number), Is.EqualTo(expectedResult));
        }

        [TestCase(5, 12, 17)]
        [TestCase(33, 44, 77)]
        public void ShouldAddTwoNumbers(int number, int secondNumber, int expectedResult)
        {
            StringCalculator cs = new StringCalculator();

            Assert.That(cs.Add(number.ToString() + "," + secondNumber.ToString()), Is.EqualTo(expectedResult));
        }

        [Test, Repeat(10)]
        public void ShouldAddAnyNumberOfNumbers()
        {
            StringCalculator cs = new StringCalculator();

            List<int> numbers = new List<int>();
            Random rnd = new Random();
            for(int i = 0; i < rnd.Next(10, 100); i++)
            {
                numbers.Add(rnd.Next(0, 100));
            }

            Assert.That(cs.Add(string.Join(",", numbers.Select(n => n.ToString()).ToArray())), Is.EqualTo(numbers.Sum()));
        }

        [Test]
        public void ShouldHandleCustomDelimiter()
        {
            StringCalculator cs = new StringCalculator();

            Assert.That(cs.Add("//x\n16x12"), Is.EqualTo(28));
        }

        [Test]
        public void ShouldNotAcceptNegativeNumbers()
        {
            StringCalculator cs = new StringCalculator();

            Assert.Throws<NegativesNotAllowedException>(() => cs.Add("-100,5,-20"));
        }

        [TestCase("123,321,12312", 444)]
        [TestCase("123,0,12312", 123)]
        [TestCase("1288888", 0)]
        public void ShouldIgnoreNumbersOver1000(string numbers, int expectedResult)
        {
            StringCalculator cs = new StringCalculator();

            Assert.That(cs.Add(numbers), Is.EqualTo(expectedResult));
        }

        [TestCase("//[abc][qwe]\n123abc321qwe111", 555)]
        [TestCase("//[abc][qwe]\n111", 111)]
        public void ShouldAcceptMultipleDelimiters(string numbers, int expectedResult)
        {
            StringCalculator cs = new StringCalculator();

            Assert.That(cs.Add(numbers), Is.EqualTo(expectedResult));
        }
    }
}