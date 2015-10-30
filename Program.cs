using System;
using ConsoleApplication1.Expression;

namespace ConsoleApplication1
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
