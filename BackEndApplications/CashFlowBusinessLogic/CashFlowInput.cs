using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CashFlowBusinessLogic
{
    public class CashFlowInput
    {
        public double InitialInvestment { get; set; }
        public double DiscountRate { get; set; }
        public double LowerBoundDiscountRate { get; set; }
        public double UpperBoundDiscountRate { get; set; }
        public double DiscountRateIncrement { get; set; }
        public List<double> CashFlow { get; set; }
    }
}
