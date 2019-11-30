using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace Calculator
{
    public static class ComputerExecutor 
    {
        public static Computer Instance { get; } = new Computer();
    }
    public class Computer : IOperation
    {
        private static readonly List<IOperation> Operations = new List<IOperation>();
        static Computer()
        {
            Operations.Add(new Parenthesis());
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
}
