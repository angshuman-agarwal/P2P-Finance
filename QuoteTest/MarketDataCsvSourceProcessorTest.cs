#region License
// Copyright (c) Angshuman Agarwal, All rights reserved.
// See License.txt in the project root for license information.
#endregion
using System;
using System.Collections.Generic;
using System.IO;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Rhino.Mocks;
using LoanApp.Angshuman;

namespace QuoteTest
{
    [TestClass]
    public class MarketDataCsvSourceProcessorTest
    {
        [TestMethod]
        public void MarketDataCsvSourceProcessor_WhenValidatorIsNull_ThrowsArgumentNullException()
        {
            ExceptionAssert.Throws<ArgumentNullException>(() => new MarketDataCsvSourceProcessor(null, null));
        }

        [TestMethod]
        public void MarketDataCsvSourceProcessor_WhenMarketDataFileDoesNotExists_ThrowsFileNotFoundException()
        {
            var mockMarketDataParser = MockRepository.GenerateMock<IMarketDataParser>();
            var mockMarketDataSourceValidator = MockRepository.GenerateMock<IMarketDataSourceValidator>();
            mockMarketDataSourceValidator.Stub(m => m.MarketDataSourceExists()).Return(false);
            ExceptionAssert.Throws<FileNotFoundException>(() => new MarketDataCsvSourceProcessor(mockMarketDataSourceValidator,mockMarketDataParser));
        }

        [TestMethod]
        public void MarketDataCsvSourceProcessor_WhenMarketDataFileIsNotACSVFile_ThrowsIOException()
        {
            var mockMarketDataParser = MockRepository.GenerateMock<IMarketDataParser>();
            var mockMarketDataSourceValidator = MockRepository.GenerateMock<IMarketDataSourceValidator>();
            mockMarketDataSourceValidator.Stub(m => m.MarketDataSourceExists()).Return(true);
            mockMarketDataSourceValidator.Stub(m => m.MarketDataSourceIsValid()).Return(false);
            ExceptionAssert.Throws<IOException>(() => new MarketDataCsvSourceProcessor(mockMarketDataSourceValidator, mockMarketDataParser));
        }

        [TestMethod]
        public void MarketDataCsvSourceProcessor_WhenCSVFileIsEmpty_ThrowsException()
        {
            var mockMarketDataParser = MockRepository.GenerateMock<IMarketDataParser>();
            var mockMarketDataSourceValidator = MockRepository.GenerateMock<IMarketDataSourceValidator>();
            mockMarketDataSourceValidator.Stub(m => m.MarketDataSourceExists()).Return(true);
            mockMarketDataSourceValidator.Stub(m => m.MarketDataSourceIsValid()).Return(true);
            mockMarketDataSourceValidator.Stub(m => m.MarketDataSourceIsEmpty()).Return(true);
            ExceptionAssert.Throws<Exception>(() => new MarketDataCsvSourceProcessor(mockMarketDataSourceValidator, mockMarketDataParser));
        }

        [TestMethod]
        public void MarketDataCsvSourceProcessor_WhenDataSourceContainsOneLender_ReturnsValidMarketDataWithOneLender()
        {
            var mockMarketDataParser = MockRepository.GenerateMock<IMarketDataParser>();
            var mockMarketDataSourceValidator = MockRepository.GenerateMock<IMarketDataSourceValidator>();
            mockMarketDataSourceValidator.Stub(m => m.MarketDataSourceExists()).Return(true);
            mockMarketDataSourceValidator.Stub(m => m.MarketDataSourceIsValid()).Return(true);
            mockMarketDataSourceValidator.Stub(m => m.MarketDataSourceIsEmpty()).Return(false);
            
            var marketDataSource = new MarketDataCsvSourceProcessor(mockMarketDataSourceValidator, mockMarketDataParser);
            var mockLender = CreateMockLender(500, 0.05);
            var mockMarketData = CreateMockMarketData(mockLender);
            mockMarketDataParser.Stub(m => m.Parse()).Return(new List<IMarketData> {mockMarketData});

            var marketData = marketDataSource.GetMarketData();

            Assert.AreEqual(1, marketData.Count);
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
