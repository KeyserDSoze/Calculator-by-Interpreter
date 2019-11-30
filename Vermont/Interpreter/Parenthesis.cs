using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace Calculator
{
    public class Parenthesis : Operation
    {
        private static readonly char[] operations = new char[2] { '(', ')' };
        public override char[] Operations => operations;
        private static readonly Regex Regex = new Regex(@"\([^\)]*\)");
        public override Context Calculate(Context context)
        {
            string finalEntry = context.Entry;
            while (finalEntry.Contains(Operations[0]))
            {
                StringBuilder match = null;
                for (int i = 0; i < finalEntry.Length; i++)
                {
                    if (finalEntry[i] == Operations[0])
                        match = new StringBuilder();
                    else if (finalEntry[i] == Operations[1])
                        break;
                    else if (match != null)
                        match.Append(finalEntry[i]);
                }
                string value = match.ToString();
                Context contextResult = ComputerExecutor.Instance.Calculate(new Context(value.Trim('(').Trim(')')));
                finalEntry = finalEntry.Replace($"({value})", contextResult.Entry);
            }
            return new Context(finalEntry);
        }
    }
}
