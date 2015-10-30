using System;

namespace Calculator.Expression
{
    public interface IExpression
    {
        double Evaluate();
    }

    public class ParameterExpression : IExpression
    {
        public double Parameter { get; }

        public ParameterExpression(double parameter)
        {
            Parameter = parameter;
        }

        public double Evaluate()
        {
            return Parameter;
        }
    }

    public class OperationExpression: IExpression
    {
        public IExpression Left { get; set;  }
        public IExpression Right { get; set; }
        public Operand Operand { get; }

        public OperationExpression(IExpression left, Operand operand, IExpression right)
        {
            Operand = operand;
            Left = left;
            Right = right;
        }
        public OperationExpression(IExpression left, Operand operand)
        {
            Operand = operand;
            Left = left;
        }

        public OperationExpression(Operand operand)
        {
            Operand = operand;
        }

        public double Evaluate()
        {
            switch (Operand)
            {
                case Operand.Plus:
                    return Left.Evaluate() + Right.Evaluate();
                case Operand.Minus:
                    return Left.Evaluate() - Right.Evaluate();
                case Operand.Multiply:
                    return Left.Evaluate() * Right.Evaluate();
                case Operand.Divide:
                    return Left.Evaluate() / Right.Evaluate();
                default:
                    throw new NotImplementedException("Operation not supported");
            }
        } 
    }

    public enum Operand
    {
        Plus,
        Minus,
        Multiply,
        Divide
    }
}
