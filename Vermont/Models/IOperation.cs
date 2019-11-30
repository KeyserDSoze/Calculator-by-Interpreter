using System;
using System.Collections.Generic;
using System.Text;

namespace Calculator
{
    public interface IOperation
    {
        Context Calculate(Context context);
    }
}
