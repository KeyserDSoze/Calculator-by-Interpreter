using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Calculator
{
    public abstract class Operation : IOperation
    {
        private static string Number = "0123456789";
        public abstract char[] Operations { get; }
        public virtual Context Calculate(Context context)
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
}
