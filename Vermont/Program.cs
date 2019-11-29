using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace Vermont
{
    class Program
    {
        private static Regex Regex = new Regex("[^0-9]");
        private static Factory Factory = new Factory();
        static void Main(string[] args)
        {
            //string ready = Console.ReadLine(); //2+5+6+9*5
            string ready = "2+5+6+9*4/2";
            IEnumerable<decimal> values = Regex.Split(ready).Select(x => decimal.Parse(x)).Reverse();
            System.Collections.IEnumerator operations = Regex.Matches(ready).Select(x => x.Value).Reverse().GetEnumerator();
            Calculator next = null;
            foreach (decimal value in values)
            {
                operations.MoveNext();
                next = Factory.Create(value, operations.Current as string).SetNext(next);
            }
            Console.WriteLine(next.Calculate());
        }
    }
}
