using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace Vermont
{
    public interface IComputer
    {
        Context Calculate(Context context);
    }
    public abstract class Operation : IComputer
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
                    for (int j = i - 1; j > 0; j--)
                        if (Number.Contains(entry[j]))
                            firstNumber = $"{entry[j]}{firstNumber}";
                        else
                            break;
                    for (int j = i + 1; j < entry.Length; j++)
                        if (Number.Contains(entry[j]))
                            firstNumber = $"{firstNumber}{entry[j]}";
                        else
                            break;
                    decimal leftNumber = decimal.Parse(firstNumber);
                    decimal rightNumber = decimal.Parse(secondNumber);
                    string result = this.GetResult(leftNumber, rightNumber, entry[i]).ToString();
                    entry = entry.Replace($"{firstNumber}{secondNumber}", result);
                    i = i - (firstNumber.Length + secondNumber.Length + 1) + result.Length;
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
    public class Context
    {
        public decimal Result { get; }
        public string Entry { get; }
        public Context(string entry, decimal result = 0)
        {
            this.Entry = entry;
            this.Result = result;
        }
    }
}
