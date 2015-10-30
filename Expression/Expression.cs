using System;

namespace ConsoleApplication1.Expression
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
        public IExpression Left { get; }
        public IExpression Right { get; }
        public Operand Operand { get; }

        public OperationExpression(IExpression left, Operand operand, IExpression right)
        {
            Operand = operand;
            Left = left;
            Right = right;
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
