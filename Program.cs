using System;
using Calculator.Expression;

namespace Calculator
{
    class Program
    {
        static void Main()
        {
            while (true)
            {
                var expressionString = Console.ReadLine();
                var parser = new ExpressionParser(expressionString);
                var expression = parser.Parse();
                var result = expression.Evaluate();

                Console.WriteLine($"The outcome is {result}");
            }
        }
    }
}
