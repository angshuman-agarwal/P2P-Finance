#region License
// Copyright (c) Angshuman Agarwal, All rights reserved.
// See License.txt in the project root for license information.
#endregion
using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Rhino.Mocks;
using LoanApp.Angshuman;

namespace QuoteTest
{
    [TestClass]   
    public class MarketDataRateCalculatorTest
    {
        [TestMethod]
        public void MarketDataRateCalculator_WhenMarketDataIsNull_ThrowsArgumentNullException()
        {
            ExceptionAssert.Throws<ArgumentNullException>(() => new MarketDataRateCalculator(null));
        }

        [TestMethod]
        public void MarketDataRateCalculator_WhenRequestedAmountIsZero_ThrowsException()
        {
            const double requestedAmt = 0;
            const double lenderAmt = 1000.0d;
            const double lenderRate = 0.07d;

            var mockMarketData = MockRepository.GenerateMock<IMarketData>();
            var mockLender = CreateMockLender(lenderAmt, lenderRate);
            mockMarketData.Stub(m => m.Lender).Return(mockLender);

            var calculator = new MarketDataRateCalculator(new List<IMarketData> { mockMarketData });
            ExceptionAssert.Throws<Exception>(() => calculator.CalculateBestRate(requestedAmt));
        }

        [TestMethod]
        public void MarketDataRateCalculator_WhenRequestedAmountIsMoreThanTotalMarketMoney_ThrowsException()
        {
            const double requestedAmt = 1200;
            const double lenderAmt = 1000.0d;
            const double lenderRate = 0.07d;

            var mockMarketData = MockRepository.GenerateMock<IMarketData>();
            var mockLender = CreateMockLender(lenderAmt, lenderRate);
            mockMarketData.Stub(m => m.Lender).Return(mockLender);

            var calculator = new MarketDataRateCalculator(new List<IMarketData> { mockMarketData });
            ExceptionAssert.Throws<Exception>(() => calculator.CalculateBestRate(requestedAmt));
        }

        [TestMethod]
        public void CalculateBestRate_WhenRequestedAmountIsEqualToLenderAmtAndThereIsOnlyOneLender_ReturnsLenderRate()
        {
            const double requestedAmt = 1000.0d;
            const double lenderAmt = 1000.0d;
            const double lenderRate = 0.07d;

            var mockMarketData = MockRepository.GenerateMock<IMarketData>();
            var mockLender = CreateMockLender(lenderAmt, lenderRate);
            mockMarketData.Stub(m => m.Lender).Return(mockLender);

            var calculator = new MarketDataRateCalculator(new List<IMarketData> {mockMarketData});
            var rate = calculator.CalculateBestRate(requestedAmt);

            Assert.AreEqual(0.07, rate);
        }

        [TestMethod]
        public void CalculateBestRate_WhenRequestedAmountIsLessThanLenderAmtAndThereAre2Lenders_ReturnsWeightedRate()
        {
            const double requestedAmt = 1000.0d;

            var mockLender1 = CreateMockLender(600, 0.05);
            var mockMarketData1 = CreateMockMarketData(mockLender1);

            var mockLender2 = CreateMockLender(400, 0.07);
            var mockMarketData2 = CreateMockMarketData(mockLender2);

            var calculator = new MarketDataRateCalculator(new List<IMarketData> { mockMarketData1, mockMarketData2 });
            var rate = calculator.CalculateBestRate(requestedAmt);

            Assert.AreEqual(0.058, rate);
        }


        [TestMethod]
        public void CalculateBestRate_WhenLenderIsLeftWithSomeMoneyAfterLendingToBorrower_ReturnsWeightedRateUsingTheUtilisedMoney()
        {
            const double requestedAmt = 1200.0d;

            var mockLender1 = CreateMockLender(700, 0.10);
            var mockMarketData1 = CreateMockMarketData(mockLender1);

            var mockLender2 = CreateMockLender(800, 0.04);
            var mockMarketData2 = CreateMockMarketData(mockLender2);

            var calculator = new MarketDataRateCalculator(new List<IMarketData> { mockMarketData1, mockMarketData2 });
            var rate = calculator.CalculateBestRate(requestedAmt);

            Assert.AreEqual(0.06, rate);
        }

        private static IMarketData CreateMockMarketData(ILender mockLender)
        {
            var mockMarketData = MockRepository.GenerateMock<IMarketData>();
            mockMarketData.Stub(m => m.Lender).Return(mockLender);
            return mockMarketData;
        }

        private static ILender CreateMockLender(double lenderAmt, double lenderRate)
        {
            var mockLender = MockRepository.GenerateMock<ILender>();
            mockLender.Stub(l => l.Name).Return("Bob");
            mockLender.Stub(l => l.ActualAmount).Return(lenderAmt);
            mockLender.Stub(l => l.Rate).Return(lenderRate);
            mockLender.Stub(l => l.RemainingAmount).Return(0.0);

            return mockLender;
        }
    }
}
