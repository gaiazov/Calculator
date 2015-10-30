using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ConsoleApplication1.Expression
{
    public class ExpressionParser
    {
        private static readonly IList<char> _operands = new[] { '-', '+', '/', '*'};


        private readonly Stack<char> _expression = new Stack<char>();

        public ExpressionParser(string expression)
        {
            foreach (var c in expression.Reverse())
            {
                _expression.Push(c);
            }


        }

        public IExpression Parse()
        {
            IExpression expr = null;
            var c = _expression.Peek();

            while (HasMoreChars)
            {
                if (char.IsDigit(c))
                {
                    expr = ParseParameterExpression(ReadParameterExpression());
                }

                else if (IsOperand(c))
                {
                    var operand = ParseOperand(_expression.Pop());
                    
                    var right = Parse();

                    expr = new OperationExpression(expr, operand, right);
                }
                else
                {
                    throw new NotImplementedException();
                }

                if (HasMoreChars)
                {
                    c = _expression.Peek();
                }
            }

            return expr;
        }

        internal static bool IsOperand(char c)
        {
            return _operands.Contains(c);
        }

        internal Operand ParseOperand(char c)
        {
            switch (c)
            {
                case '-':
                    return Operand.Minus;
                case '+':
                    return Operand.Plus;
                case '/':
                    return Operand.Divide;
                case '*':
                    return Operand.Multiply;
                default:
                    throw new NotImplementedException($"Cannot parse {c} as an operand");
            }
        }

        internal string ReadParameterExpression()
        {
            var c = _expression.Pop();
            var expression = new StringBuilder();
            expression.Append(c);

            while (HasMoreChars && char.IsDigit(_expression.Peek()))
            {
                c = _expression.Pop();
                expression.Append(c);
            }

            return expression.ToString();
        }

        private bool HasMoreChars => _expression.Count > 0;

        internal IExpression ParseParameterExpression(string expression)
        {
            double value;

            if (!double.TryParse(expression, out value))
            {
                throw new InvalidOperationException($"Couldn't parse expression {expression} as a double");
            }

            return new ParameterExpression(value);
        }
    }
}
