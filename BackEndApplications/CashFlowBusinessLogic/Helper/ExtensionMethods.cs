using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CashFlowBusinessLogic.Helper
{
    public static class ExtensionMethods
    {
        public static double ToPercentage(this double value)
        {
            return (value > 0) ? value /= 100 : value;
        }
    }
}
