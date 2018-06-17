using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Practices.Unity;
using CashFlowBusinessLogic;

namespace CashFlowUnitTest
{
    [TestClass]
    public class UnitTest
    {

        [TestMethod]
        public void Test_CustomCashFlowService_Case1()
        {
            IUnityContainer container = new UnityContainer();
            container.RegisterType<ICashFlowCalculator, CashFlowCalculator>();

            double[] _terms = { 1000, 1000, 1000 };

            CashFlowInput _input = new CashFlowInput()
            {
                InitialInvestment = 100000,
                LowerBoundDiscountRate = 1,
                UpperBoundDiscountRate = 15,
                DiscountRateIncrement = .25,
                CashFlow = _terms.ToList()
            };
            
            CashFlowClient client = container.Resolve<CashFlowClient>();
            var result = client.GetNetPresentValue(_input);

            Assert.AreEqual(result.NetPresentValue, -99962.5);
        }


        [TestMethod]
        public void Test_CustomCashFlowService_Case2()
        {
            IUnityContainer container = new UnityContainer();
            container.RegisterType<ICashFlowCalculator, CashFlowCalculator>();

            double[] _terms = { 110000, 110000, 110000 };

            CashFlowInput _input = new CashFlowInput()
            {
                InitialInvestment = 100000,
                LowerBoundDiscountRate = 1,
                UpperBoundDiscountRate = 15,
                DiscountRateIncrement = .25,
                CashFlow = _terms.ToList()
            };

            CashFlowClient client = container.Resolve<CashFlowClient>();
            var result = client.GetNetPresentValue(_input);

            Assert.AreEqual(result.NetPresentValue, -95875);
        }
    }
}
