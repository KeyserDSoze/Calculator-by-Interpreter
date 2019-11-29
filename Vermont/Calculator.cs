using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace Vermont
{
    public interface ICalculator
    {
        decimal Calculate(decimal result = 0);
    }
    public abstract class Calculator : ICalculator
    {
        private Calculator Next;
        private decimal Value;
        public Calculator(decimal value) => this.Value = value;
        public decimal Calculate(decimal result = 0)
        {
            result = this.InnerCalculate(result, this.Value);
            if (Next != null)
                return Next.Calculate(result);
            return result;
        }
        private protected abstract decimal InnerCalculate(decimal a, decimal b);

        internal Calculator SetNext(Calculator next)
        {
            this.Next = next;
            return this;
        }
    }

    public class Summer : Calculator
    {
        public Summer(decimal value) : base(value)
        {
        }

        private protected override decimal InnerCalculate(decimal a, decimal b)
            => a + b;
    }
    public class Subtracter : Calculator
    {
        public Subtracter(decimal value) : base(value)
        {
        }

        private protected override decimal InnerCalculate(decimal a, decimal b)
            => a - b;
    }
    public class Multiplier : Calculator
    {
        public Multiplier(decimal value) : base(value)
        {
        }

        private protected override decimal InnerCalculate(decimal a, decimal b)
            => a * b;
    }
    public class Divider : Calculator
    {
        public Divider(decimal value) : base(value)
        {
        }

        private protected override decimal InnerCalculate(decimal a, decimal b)
            => a / b;
    }
    public class Root  : Calculator
    {
        public Root(decimal value) : base(value)
        {
        }

        private protected override decimal InnerCalculate(decimal a, decimal b)
        {
            return b;
        }
    }
    public class Factory
    {
        public Calculator Create(decimal value, string operation)
        {
            switch (operation)
            {
                case "+":
                    return new Summer(value);
                case "-":
                    return new Subtracter(value);
                case "*":
                    return new Multiplier(value);
                case "/":
                    return new Divider(value);
                case null:
                    return new Root(value);
                default:
                    throw new ArgumentException($"Operation {operation} not supported.");
            }
        }
    }
}
