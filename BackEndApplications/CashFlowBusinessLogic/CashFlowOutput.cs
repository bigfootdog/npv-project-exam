using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CashFlowBusinessLogic
{
    public class CashFlowOutput
    {
        public double NetPresentValue { get; set; }
        public List<CashFlow> CashFlows { get; set; }
    }

    public class CashFlow {
        public int index { get; set; }
        public double Amount { get; set; }
        public double Net { get; set; }
        public double Discount { get; set; }
    }

}
