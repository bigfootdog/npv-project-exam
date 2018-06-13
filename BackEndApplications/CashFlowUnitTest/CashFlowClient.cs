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
        public double GetNetPresentValue(CashFlowInput param)
        {
            return this._service.GetNetPresentValue(param);
        }
    }
}
