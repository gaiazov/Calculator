using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Calculator.Expression;
using FluentAssertions;
using NUnit.Framework;

namespace Calculator.Tests
{
    [TestFixture]
    internal class ExcerciseTests
    {
        [Test]
        public void Excercise_Tests_Should_All_Return_Expected_Result()
        {
            var tests = new Dictionary<string, double>
            {
                {"1 + 2", 1 + 2},
                {"5 - 3", 5 - 3},
                {"55 - 5", 55 - 5},
                {"2 + 5 - 5 + 20", 2 + 5 - 5 + 20},
                {"(5 + 3)*4", (5 + 3)*4},
                {"(5 + 3)*(4 - 1)", (5 + 3)*(4 - 1)},
                {"5 + 5*2", 5 + 5*2},
                {"5 + 5*2 - 1", 5 + 5*2 - 1},
                {"5.5*2", 5.5*2},
                {"5.5*2.5", 5.5*2.5},
                {"5/2", 5/2d},
                {"5*5", 5*5}
            };

            foreach (var test in tests)
            {
                RunExpressionTest(test.Key, test.Value);
            }
        }

        private static void RunExpressionTest(string expression, double outcome)
        {
            var parsedExpression = new ExpressionParser(expression).Parse();

            var result = parsedExpression.Evaluate();

            result.Should().Be(outcome, $"expression '{expression}' should evaluate to {outcome}");

            Console.WriteLine($"{expression} = {result}");
        }
    }
}
