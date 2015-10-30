using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ConsoleApplication1.Expression;

namespace ConsoleApplication1
{
    public class Calculator
    {
        public double Calculate(string expressionString)
        {
            var parser = new ExpressionParser(expressionString);
            var expression = parser.Parse();
            var result = expression.Evaluate();
            return result;
        }
    }
}
