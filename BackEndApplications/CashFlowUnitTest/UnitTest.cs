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

            double[] cashFlow = { 50000, 25000 };
            double intital = 150000;

            CashFlowClient client = container.Resolve<CashFlowClient>();
            var result = client.GetNetPresentValue(1.2, cashFlow.ToList()) - intital;

            Assert.AreEqual(Math.Round(result, 2), -76182.26);
        }

        [TestMethod]
        public void Test_CustomCashFlowService_Case2()
        {
            IUnityContainer container = new UnityContainer();
            container.RegisterType<ICashFlowCalculator, CashFlowCalculator>();

            double[] cashFlow = { 50000, 25000 };
            double intital = 150000;

            CashFlowClient client = container.Resolve<CashFlowClient>();
            var result = client.GetNetPresentValue(1.3, cashFlow.ToList()) - intital;

            Assert.AreEqual(Math.Round(result, 2), -76279.20);
        }

        [TestMethod]
        public void Test_CustomCashFlowService_Case3()
        {
            IUnityContainer container = new UnityContainer();
            container.RegisterType<ICashFlowCalculator, CashFlowCalculator>();

            double[] cashFlow = { 50000, 25000 };
            double intital = 150000;

            CashFlowClient client = container.Resolve<CashFlowClient>();
            var result = client.GetNetPresentValue(1.4, cashFlow.ToList()) - intital;

            Assert.AreEqual(Math.Round(result, 2), -76375.90);
        }
    }
}
