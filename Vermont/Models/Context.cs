using System;
using System.Collections.Generic;
using System.Text;

namespace Calculator
{
    public class Context
    {
        public decimal Result => decimal.Parse(this.Entry);
        public string Entry { get; }
        public Context(string entry) => this.Entry = entry.Replace(" ", string.Empty);
    }
}
