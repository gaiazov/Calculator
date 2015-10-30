using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Calculator.Expression;

namespace Calculator
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
