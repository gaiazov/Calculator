using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ConsoleApplication1.Expression
{
    public class ExpressionParser
    {
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

            var left = new Stack<IExpression>();
            var operands = new Stack<Operand>();
            var right = new Stack<IExpression>(); 

            while (HasMoreChars)
            {
                var c = _expression.Peek();

                if (char.IsDigit(c))
                {
                    var parameter = ReadParameterExpression();
                    var expression = ParseParameterExpression(parameter);
                    if (left.Count == right.Count)
                    {
                        left.Push(expression);
                    }
                    else
                    {
                        right.Push(expression);
                    }
                }
                else if (IsPlusOrMinus(c) || IsTimesOrDivide(c))
                {
                    var operand = ParseOperand(_expression.Pop());

                    if (right.Any() && (operand == Operand.Divide || operand == Operand.Multiply))
                    {
                        left.Push(right.Pop());
                    }

                    if (operands.Any())
                    {
                        var currentOperand = operands.Peek();
                        if ((operand == Operand.Plus || operand == Operand.Minus) &&
                            (currentOperand == Operand.Multiply || currentOperand == Operand.Divide))
                        {
                            var expression = new OperationExpression(left.Pop(), operands.Pop(), right.Pop());

                            if (left.Count == right.Count)
                            {
                                left.Push(expression);
                            }
                            else
                            {
                                right.Push(expression);
                            }
                        }

                        if ((operand == Operand.Plus || operand == Operand.Minus) &&
                            (currentOperand == Operand.Plus || currentOperand == Operand.Minus))
                        {
                            var expression = new OperationExpression(left.Pop(), operands.Pop(), right.Pop());

                            if (left.Count == right.Count)
                            {
                                left.Push(expression);
                            }
                            else
                            {
                                right.Push(expression);
                            }
                        }
                    }

                    operands.Push(operand);
                }
                else if (IsOpeningBracket(c))
                {
                    _expression.Pop();
                    var expression = Parse();

                    if (left.Count == right.Count)
                    {
                        left.Push(expression);
                    }
                    else
                    {
                        right.Push(expression);
                    }
                }
                else if (IsClosingBracket(c))
                {
                    _expression.Pop();

                    while (operands.Any())
                    {
                        var expression = new OperationExpression(left.Pop(), operands.Pop(), right.Pop());

                        if (left.Count == right.Count)
                        {
                            left.Push(expression);
                        }
                        else
                        {
                            right.Push(expression);
                        }
                    }

                    return left.Pop();
                }
                else
                {
                    throw new NotImplementedException();
                }
            }

            while (operands.Any())
            {
                var expression = new OperationExpression(left.Pop(), operands.Pop(), right.Pop());

                if (left.Count == right.Count)
                {
                    left.Push(expression);
                }
                else
                {
                    right.Push(expression);
                }
            }

            return left.Pop();
        }

        internal static bool IsPlusOrMinus(char c)
        {
            return c == '-' || c == '+';
        }

        internal static bool IsTimesOrDivide(char c)
        {
            return c == '*' || c == '/';
        }

        internal static bool IsOpeningBracket(char c)
        {
            return c == '(';
        }

        internal static bool IsClosingBracket(char c)
        {
            return c == ')';
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

        private bool HasMoreChars => _expression.Any();

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
