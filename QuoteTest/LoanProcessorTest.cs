#region License
// Copyright (c) Angshuman Agarwal, All rights reserved.
// See License.txt in the project root for license information.
#endregion
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Rhino.Mocks;
using LoanApp.Angshuman;

namespace QuoteTest
{
    [TestClass]
    public class LoanProcessorTest
    {
        [TestMethod] //£30.78
        public void ProcessLoan_WhenRateIs7PercentAndBorrowerAmountIs1000AndTenureIs36Months_Returns30Pound10PAsMonthlyRepaymentAmount()
        {
            // Arrange
            var mockRateCalculator = MockRepository.GenerateMock<IMarketDataRateCalculator>();
            var mockMarketDataConfig = MockRepository.GenerateMock<IMarketDataConfiguration>();

            mockMarketDataConfig.Stub(c => c.LoanTenure).Return(36);
            mockMarketDataConfig.Stub(c => c.BorrowerLoanAmount).Return(1000);
            mockRateCalculator.Stub(r => r.CalculateBestRate(1000.0d)).Return(0.070039999999999991);
            var loanProcessor = new LoanProcessor(mockRateCalculator, mockMarketDataConfig);

            // Act
            loanProcessor.ProcessLoan();

            // Assert
            Assert.AreEqual(30.78, loanProcessor.MonthlyRepayment);
        }

        [TestMethod] //£1108.10
        public void ProcessLoan_WhenRateIs7PercentAndBorrowerAmountIs1000AndTenureIs36Months_Returns1108Pound10PAsTotalRepaymentAmount()
        {
            // Arrange
            var mockRateCalculator = MockRepository.GenerateMock<IMarketDataRateCalculator>();
            var mockMarketDataConfig = MockRepository.GenerateMock<IMarketDataConfiguration>();

            mockMarketDataConfig.Stub(c => c.LoanTenure).Return(36);
            mockMarketDataConfig.Stub(c => c.BorrowerLoanAmount).Return(1000);
            mockRateCalculator.Stub(r => r.CalculateBestRate(1000.0d)).Return(0.070039999999999991);
            var loanProcessor = new LoanProcessor(mockRateCalculator, mockMarketDataConfig);

            // Act
            loanProcessor.ProcessLoan();

            // Assert
            Assert.AreEqual(1108.10, loanProcessor.TotalRepayment);
        }
    }
}
