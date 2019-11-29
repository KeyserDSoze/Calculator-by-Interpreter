using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace Vermont
{
    public class Computer : IOperation
    {
        private static readonly List<IOperation> Operations = new List<IOperation>();
        static Computer()
        {
            Operations.Add(new MultiplyDivider());
            Operations.Add(new AddSubtracter());
        }
        public Context Calculate(Context context)
        {
            foreach (IOperation operation in Operations)
                context = operation.Calculate(context);
            return context;
        }
    }
    public interface IOperation
    {
        Context Calculate(Context context);
    }

    public abstract class Operation : IOperation
    {
        private static string Number = "0123456789";
        public abstract char[] Operations { get; }
        public Context Calculate(Context context)
        {
            string entry = context.Entry;
            for (int i = 0; i < entry.Length; i++)
            {
                if (Operations.Contains(entry[i]))
                {
                    string firstNumber = string.Empty;
                    string secondNumber = string.Empty;
                    for (int j = i - 1; j >= 0; j--)
                        if (Number.Contains(entry[j]))
                            firstNumber = $"{entry[j]}{firstNumber}";
                        else
                            break;
                    for (int j = i + 1; j < entry.Length; j++)
                        if (Number.Contains(entry[j]))
                            secondNumber = $"{secondNumber}{entry[j]}";
                        else
                            break;
                    decimal leftNumber = decimal.Parse(firstNumber);
                    decimal rightNumber = decimal.Parse(secondNumber);
                    string result = this.GetResult(leftNumber, rightNumber, entry[i]).ToString();
                    entry = entry.Replace($"{firstNumber}{entry[i]}{secondNumber}", result);
                    i = i - (firstNumber.Length + secondNumber.Length) + result.Length;
                }
            }
            return new Context(entry);
        }
        private decimal GetResult(decimal left, decimal right, char operation)
        {
            switch (operation)
            {
                case '+':
                    return left + right;
                case '-':
                    return left - right;
                case '*':
                    return left * right;
                case '/':
                    return left / right;
                default:
                    throw new ArgumentException($"Operation {operation} is not implemented.");
            }
        }
    }
    public class MultiplyDivider : Operation
    {
        public override char[] Operations => new char[2] { '*', '/' };
    }
    public class AddSubtracter : Operation
    {
        public override char[] Operations => new char[2] { '+', '-' };
    }
    public class Context
    {
        public decimal Result => decimal.Parse(this.Entry);
        public string Entry { get; }
        public Context(string entry) => this.Entry = entry;
    }
}
