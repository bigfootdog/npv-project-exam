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
        List<CashFlowOutput> GetMultipleNetPresentValues(CashFlowInput entry);
        double GetNetPresentValue(double discount, List<double> cashflow);
    }

    public class CashFlowCalculator : ICashFlowCalculator
    {
        public List<CashFlowOutput> GetMultipleNetPresentValues(CashFlowInput entry)
        {
            List<CashFlowOutput> result = new List<CashFlowOutput>();

            try
            {
                double discount = entry.LowerBoundDiscountRate;

                while (discount < (entry.UpperBoundDiscountRate + entry.DiscountRateIncrement))
                {
                    double netPresentValueByRate = 0;

                    netPresentValueByRate = this.GetNetPresentValue(discount, entry.CashFlow);

                    result.Add(new CashFlowOutput()
                    {
                        NetPresentValue = Math.Round(netPresentValueByRate - entry.InitialInvestment, 2),
                        Discount = Math.Round(discount, 2)
                    });

                    discount += entry.DiscountRateIncrement;
                }

            }
            catch (Exception ex)
            {
                //TODOs: Add logging error
                Console.WriteLine(ex.InnerException.ToString());
            }

            return result;
        }

        public double GetNetPresentValue(double discount, List<double> cashflow)
        {
            double netPresentValue = 0;

            try
            {
                foreach (var term in cashflow.Select((amount, index) => new { index, amount }))
                {
                    var discountedAmount = term.amount / Math.Pow(1 + (discount.ToPercentage()), term.index + 1);
                    netPresentValue += (double)(decimal)discountedAmount;
                }

            }
            catch (Exception ex)
            {
                //TODOs: Add logging error
                Console.WriteLine(ex.InnerException.ToString());
            }

            return Math.Round(netPresentValue, 2);
        }
    }
}
