using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using CashFlowBusinessLogic;

namespace CashFlowWebApi.Controllers
{
    public class CashFlowController : ApiController
    {
        private readonly ICashFlowCalculator _cashFlowCalculator;

        public CashFlowController(ICashFlowCalculator cashFlowCalculator)
        {
            this._cashFlowCalculator = cashFlowCalculator;
        }

        [Route("api/cashflow/getnpv")]
        [HttpPost]
        public List<CashFlowOutput> Post(CashFlowInput entry)
        {
            return this._cashFlowCalculator.GetMultipleNetPresentValues(entry);
        }
    }
}
