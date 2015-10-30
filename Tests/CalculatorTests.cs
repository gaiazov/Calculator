using FluentAssertions;
using NUnit.Framework;

namespace Calculator.Tests
{
    [TestFixture]
    class CalculatorTests
    {
        [Test]
        public void Calculator_Should_Calculate_Test1()
        {
            var calculator = new Calculator();
            var result = calculator.Calculate("(5+3)*4");

            result.Should().Be(32);
        }

        [Test]
        public void Calculator_Should_Calculate_Test2()
        {
            var calculator = new Calculator();
            var result = calculator.Calculate("(5+3)*(4-1)");

            result.Should().Be(24);
        }

        [Test]
        public void Calculator_Should_Calculate_Test3()
        {
            var calculator = new Calculator();
            var result = calculator.Calculate("5+5*2");

            result.Should().Be(15);
        }

        [Test]
        public void Calculator_Should_Calculate_Test4()
        {
            var calculator = new Calculator();
            var result = calculator.Calculate("5+5*2-1");

            result.Should().Be(14);
        }
    }
}
