using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CashFlowBusinessLogic;

namespace CashFlowUnitTest
{
    public class CashFlowClient
    {
        ICashFlowCalculator _service;
        public CashFlowClient(ICashFlowCalculator service)
        {
            this._service = service;
        }

        public List<CashFlowOutput> GetMultipleNetPresentValues(CashFlowInput entry)
        {
            return this._service.GetMultipleNetPresentValues(entry);
        }

        public double GetNetPresentValue(double discount, List<double> cashFlow)
        {
            return this._service.GetNetPresentValue(discount, cashFlow);
        }
    }
}
