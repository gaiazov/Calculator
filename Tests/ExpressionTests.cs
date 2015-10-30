using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Calculator.Expression;
using NUnit.Framework;
using FluentAssertions;

namespace Calculator.Tests
{
    [TestFixture]
    class ExpressionTests
    {
        [Test]
        public void One_Plus_One_Should_Evaluate_To_Two()
        {
            var expression = new OperationExpression(
               new ParameterExpression(1.0d),
               Operand.Plus,
               new ParameterExpression(1.0d));

            var result = expression.Evaluate();

            result.Should().Be(2d);
        }
    }
}
