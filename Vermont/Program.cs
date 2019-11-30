using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace Calculator
{
    class Program
    {
        static void Main(string[] args)
        {
            //string ready = Console.ReadLine(); //2+5+6+9*5
            string ready = "2*(5+ (6  +9)*2)*4/2";
            Computer computer = new Computer();
            Context context = computer.Calculate(new Context(ready));
            Console.WriteLine(context.Result);
        }
    }
}
