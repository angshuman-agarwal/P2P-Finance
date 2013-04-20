#region License
// Copyright (c) Angshuman Agarwal, All rights reserved.
// See License.txt in the project root for license information.
#endregion
using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Rhino.Mocks;
using LoanApp.Angshuman;

namespace QuoteTest
{
    [TestClass]
    public class MarketDataSourceFactoryTest
    {
        [TestMethod]
        public void GetMarketDataSource_WhenTheDataSourceIsNullOrEmpty_ThrowsException()
        {
            ExceptionAssert.Throws<ArgumentNullException>(() => MarketDataSourceFactory.GetMarketDataSource(null));
        }

        [TestMethod]
        public void GetMarketDataSource_WhenSourceIsCsvFile_ReturnsAValidSource()
        {
            // Arrange
            var mockMarketDataSourceValidator = MockRepository.GenerateMock<IMarketDataSourceValidator>();
            var mockMarketDataParser = MockRepository.GenerateMock<IMarketDataParser>();
            mockMarketDataSourceValidator.Stub(m => m.MarketDataSourceExists()).Return(true);
            mockMarketDataSourceValidator.Stub(m => m.MarketDataSourceIsEmpty()).Return(false);
            mockMarketDataSourceValidator.Stub(m => m.MarketDataSourceIsValid()).Return(true);
            var marketDataSrcProcessor = new MarketDataCsvSourceProcessor(mockMarketDataSourceValidator, mockMarketDataParser);
            MarketDataSourceFactory.SetMarketDataSource(marketDataSrcProcessor);
            
            // Act
            var source = MarketDataSourceFactory.GetMarketDataSource(@"Market.csv");
            
            // Assert
            Assert.AreEqual(marketDataSrcProcessor, source);
        }
    }
}
