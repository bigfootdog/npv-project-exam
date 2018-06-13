using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CashFlowBusinessLogic
{
    public interface ICashFlowCalculator
    {
        double GetNetPresentValue(CashFlowInput entry);
    }
   
    public class CashFlowCalculator : ICashFlowCalculator
    {
        public double GetNetPresentValue(CashFlowInput entry)
        {
            double _netPresentValue = 0;
            double _cashFlowsFromInvestments = 0;

            try
            {
                entry.LowerBoundDiscountRate /= 100;
                entry.UpperBoundDiscountRate /= 100;
                entry.DiscountRateIncrement /= 100;
                
                foreach (var term in entry.CashFlow.Select((amount, index) => new { index, amount }))
                {
                    double _discount = (term.index == 0) ? entry.LowerBoundDiscountRate  : (entry.LowerBoundDiscountRate + ((term.index) * entry.DiscountRateIncrement));
                    _discount = (_discount > entry.UpperBoundDiscountRate) ? entry.UpperBoundDiscountRate : _discount;
                    var discountedAmount = term.amount * _discount;
                    _cashFlowsFromInvestments += (double)Math.Round((decimal)discountedAmount, 2);
                }

                _netPresentValue = _cashFlowsFromInvestments - entry.InitialInvestment;
            }
            catch (Exception ex)
            {
                //TODOs: Add logging error
                Console.WriteLine(ex.InnerException.ToString());
            }

            return Math.Round(_netPresentValue, 2);
        }
    }
}
