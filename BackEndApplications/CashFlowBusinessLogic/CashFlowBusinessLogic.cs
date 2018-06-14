using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CashFlowBusinessLogic
{
    public interface ICashFlowCalculator
    {
        CashFlowOutput GetNetPresentValue(CashFlowInput entry);
    }
   
    public class CashFlowCalculator : ICashFlowCalculator
    {
        public CashFlowOutput GetNetPresentValue(CashFlowInput entry)
        {
            double _netPresentValue = 0;
            double _cashFlowsFromInvestments = 0;
            CashFlowOutput result = new CashFlowOutput();
            List<CashFlow> cashflows = new List<CashFlow>();

            try
            {
                entry.LowerBoundDiscountRate /= 100;
                entry.UpperBoundDiscountRate /= 100;
                entry.DiscountRateIncrement /= 100;
                
                foreach (var term in entry.CashFlow.Select((amount, index) => new { index, amount }))
                {
                    double _discount = (term.index == 0) ? entry.LowerBoundDiscountRate  : (entry.LowerBoundDiscountRate + ((term.index) * entry.DiscountRateIncrement));
                    _discount = (_discount > entry.UpperBoundDiscountRate) ? entry.UpperBoundDiscountRate : _discount;
                    var _netAmount = term.amount * _discount;
                    _cashFlowsFromInvestments += (double)Math.Round((decimal)_netAmount, 2);

                    cashflows.Add(new CashFlow() { index = term.index, Amount = term.amount, Net = _netAmount,  Discount = Math.Round(_discount *=100, 2) });
                }

                _netPresentValue = _cashFlowsFromInvestments - entry.InitialInvestment;
                result.NetPresentValue = Math.Round(_netPresentValue, 2);
                result.CashFlows = cashflows;
            }
            catch (Exception ex)
            {
                //TODOs: Add logging error
                Console.WriteLine(ex.InnerException.ToString());
            }

            return result;
        }
    }
}
