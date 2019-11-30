using System;
using System.Collections.Generic;
using System.Text;

namespace Calculator
{
    public class MultiplyDivider : Operation
    {
        public override char[] Operations => new char[2] { '*', '/' };
    }
}
