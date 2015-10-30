using Calculator.Expression;
using FluentAssertions;
using NUnit.Framework;

namespace Calculator.Tests
{
    [TestFixture]
    internal class ExpressionParserTests
    {
        [Test]
        public void Expression_Parser_Should_Parse_One_Plus_One()
        {
            var parsedExpression = new ExpressionParser("1+1").Parse();

            parsedExpression.Should()
                .BeOfType<OperationExpression>();

            var opExpression = (OperationExpression) parsedExpression;
            opExpression.Left.Should().BeOfType<ParameterExpression>();
            opExpression.Right.Should().BeOfType<ParameterExpression>();
            opExpression.Operand.Should().Be(Operand.Plus);

            parsedExpression.Evaluate().Should().Be(2d);
        }

        [Test]
        public void Expression_Parser_Should_Parse_Five_Minus_Three()
        {
            var parsedExpression = new ExpressionParser("5-3").Parse();

            parsedExpression.Should()
                .BeOfType<OperationExpression>();

            var opExpression = (OperationExpression) parsedExpression;
            opExpression.Left.Should().BeOfType<ParameterExpression>();
            opExpression.Right.Should().BeOfType<ParameterExpression>();
            opExpression.Operand.Should().Be(Operand.Minus);

            parsedExpression.Evaluate().Should().Be(2d);
        }

        [Test]
        public void Expression_Parser_Should_Parse_LargeNumber_Minus_Three()
        {
            const int num = 48017414;
            var parsedExpression = new ExpressionParser($"{num}-3").Parse();

            parsedExpression.Should()
                .BeOfType<OperationExpression>();

            var opExpression = (OperationExpression) parsedExpression;
            opExpression.Left.Should().BeOfType<ParameterExpression>();
            opExpression.Right.Should().BeOfType<ParameterExpression>();
            opExpression.Operand.Should().Be(Operand.Minus);

            parsedExpression.Evaluate().Should().Be(num - 3);
        }

        [Test]
        public void Expression_Parser_Should_Parse_Long_Operations()
        {
            var parsedExpression = new ExpressionParser("2+5-5+20").Parse();

            parsedExpression.Should()
                .BeOfType<OperationExpression>();

            parsedExpression.Should()
                .BeOfType<OperationExpression>();

            /*
            var opExpression = (OperationExpression)parsedExpression;
            opExpression.Left.Should().BeOfType<ParameterExpression>();
            opExpression.Left.As<ParameterExpression>().Parameter.Should().Be(2);
            opExpression.Operand.Should().Be(Operand.Plus);
            opExpression.Right.Should().BeOfType<OperationExpression>();

            var right1 = (OperationExpression)opExpression.Right;
            right1.Left.Should().BeOfType<ParameterExpression>();
            right1.Left.As<ParameterExpression>().Parameter.Should().Be(5);
            right1.Operand.Should().Be(Operand.Minus);
            right1.Right.Should().BeOfType<OperationExpression>();

            var right2 = (OperationExpression)right1.Right;
            right2.Left.Should().BeOfType<ParameterExpression>();
            right2.Left.As<ParameterExpression>().Parameter.Should().Be(5);
            right2.Operand.Should().Be(Operand.Plus);
            right2.Right.Should().BeOfType<ParameterExpression>();
            right2.Right.As<ParameterExpression>().Parameter.Should().Be(20);
            */


            parsedExpression.Evaluate().Should().Be(22d);
        }

        [Test]
        public void Expression_Parser_Should_Parse_Multiplication()
        {
            var parsedExpression = new ExpressionParser("5*3").Parse();
            parsedExpression.Evaluate().Should().Be(15);
        }

        [Test]
        public void Expression_Parser_Should_Parse_Multiplication_And_Addition()
        {
            var parsedExpression = new ExpressionParser("5*3+2").Parse();
            parsedExpression.Evaluate().Should().Be(17);
        }

        [Test]
        public void Expression_Parser_Should_Parse_Addition_And_Multiplication()
        {
            var parsedExpression = new ExpressionParser("9+5*3").Parse();
            parsedExpression.Evaluate().Should().Be(9 + 5*3);
        }

        [Test]
        public void Expression_Parser_Should_Parse_Brackets()
        {
            var parsedExpression = new ExpressionParser("20-(5+3)").Parse();
            parsedExpression.Evaluate().Should().Be(20 - (5 + 3));
        }

        [Test]
        public void Expression_Parser_Should_Parse_Complex_Brackets()
        {
            var parsedExpression = new ExpressionParser("20-(5*(3+4*2+2))").Parse();
            parsedExpression.Evaluate().Should().Be(20 - (5*(3 + 4*2 + 2)));
        }

        [Test]
        public void Expression_Parser_Should_Parse_Long_Multiplication_And_Addition()
        {
            var parsedExpression = new ExpressionParser("2+(3+4*2+2)").Parse();
            parsedExpression.Evaluate().Should().Be(2 + (3 + 4*2 + 2));
        }

        [Test]
        public void Expression_Parser_Should_Parse_Division()
        {
            var parsedExpression = new ExpressionParser("10/5").Parse();
            parsedExpression.Evaluate().Should().Be(10/5);
        }

        [Test]
        public void Expression_Parser_Should_Parse_Decimals()
        {
            var parsedExpression = new ExpressionParser("10.5 - 0.5").Parse();
            parsedExpression.Evaluate().Should().Be(10.5 - 0.5);
        }

        [Test]
        public void Expression_Parser_Should_Parse_Division_And_Addition()
        {
            var parsedExpression = new ExpressionParser("10/5+3").Parse();
            parsedExpression.Evaluate().Should().Be(10/5 + 3);
        }

        [Test]
        public void Expression_Parser_Should_Parse_Nested_Brackets()
        {
            var parsedExpression = new ExpressionParser("10/5 + 3 + 4*(2 - (1 + (3 - 4)/2 - 5*(1 + 2))/2 + 5)").Parse();
            parsedExpression.Evaluate().Should().Be(10/5 + 3 + 4*(2 - (1 + (3 - 4)/2d - 5*(1 + 2))/2d + 5));
        }

        [Test]
        public void Expression_Parser_Should_Parse_Nested_Nested_Brackets()
        {
            var parsedExpression = new ExpressionParser("5 + (5 - (5 + (5 - 4))) + 2").Parse();
            parsedExpression.Evaluate().Should().Be(5 + (5 - (5 + (5 - 4))) + 2);
        }

        [Test]
        public void Expression_Parser_Should_Parse_Fifty_Five_Minus_Five()
        {
            var parsedExpression = new ExpressionParser("55-5").Parse();

            parsedExpression.Should()
                .BeOfType<OperationExpression>();

            var opExpression = (OperationExpression) parsedExpression;
            opExpression.Left.Should().BeOfType<ParameterExpression>();
            opExpression.Left.Evaluate().Should().Be(55d);
            opExpression.Right.Should().BeOfType<ParameterExpression>();
            opExpression.Right.Evaluate().Should().Be(5d);
            opExpression.Operand.Should().Be(Operand.Minus);

            parsedExpression.Evaluate().Should().Be(50d);
        }

        [Test]
        public void Expression_Parser_Should_Parse_One()
        {
            var parsedExpression = new ExpressionParser("1").Parse();

            parsedExpression.Should()
                .BeOfType<ParameterExpression>();

            parsedExpression.Evaluate().Should().Be(1d);

        }

        [Test]
        public void Expression_Parser_Should_Parse_Eleven()
        {
            var parsedExpression = new ExpressionParser("11").Parse();

            parsedExpression.Should()
                .BeOfType<ParameterExpression>();

            parsedExpression.Evaluate().Should().Be(11d);

        }

        [Test]
        public void Expression_Parser_Should_Parse_Twenty()
        {
            var parsedExpression = new ExpressionParser("20").Parse();

            parsedExpression.Should()
                .BeOfType<ParameterExpression>();

            parsedExpression.Evaluate().Should().Be(20);

        }
    }
}