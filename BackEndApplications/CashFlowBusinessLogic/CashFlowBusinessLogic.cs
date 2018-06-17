using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CashFlowBusinessLogic.Helper;

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
            double _cashFlowsFromInvestments = 0;
            CashFlowOutput _response = new CashFlowOutput();
            List<CashFlow> _cashflows = new List<CashFlow>();

            try
            {
                foreach (var term in entry.CashFlow.Select((amount, index) => new { index, amount }))
                {
                    double _discount = (term.index == 0) ? entry.LowerBoundDiscountRate.ToPercentage() : (entry.LowerBoundDiscountRate.ToPercentage() + ((term.index) * entry.DiscountRateIncrement.ToPercentage()));
                    _discount = (_discount > entry.UpperBoundDiscountRate.ToPercentage()) ? entry.UpperBoundDiscountRate.ToPercentage() : _discount;
                    var _netAmount = term.amount * _discount;
                    _cashFlowsFromInvestments += (double)Math.Round((decimal)_netAmount, 2);

                    _cashflows.Add(new CashFlow() { index = term.index, Amount = term.amount, Net = _netAmount,  Discount = Math.Round(_discount *=100, 2) });
                }

                _response.NetPresentValue = Math.Round(_cashFlowsFromInvestments - entry.InitialInvestment, 2);
                _response.CashFlows = _cashflows;
            }
            catch (Exception ex)
            {
                //TODOs: Add logging error
                Console.WriteLine(ex.InnerException.ToString());
            }

            return _response;
        }

    }    
}
