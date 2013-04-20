#region License
// Copyright (c) Angshuman Agarwal, All rights reserved.
// See License.txt in the project root for license information.
#endregion
using System;

namespace LoanApp.Angshuman
{
    /// <summary>
    /// Factory class responsible for creating the appropriate data source processor based on the input market data source.
    /// The market data source can be an external web service or a csv file or a database , etc...
    /// </summary>
    public sealed class MarketDataSourceFactory
    {
        private static IMarketDataSourceProcessor _marketDataProcessor;

        public static IMarketDataSourceProcessor GetMarketDataSource(string sourceLocation)
        {
            if (String.IsNullOrEmpty(sourceLocation))
            {
                throw new ArgumentNullException("sourceLocation");
            }
            
            // assume there is a parser which parses the processor and extracts appropriate information from the sourceLocation for identifying
            // various processor types, one which is CSV
            // For this problem and simplicity sake, we are taking csv into consideration directly.
            const string sourceType = "CSV";

            switch (sourceType)
            {
                case "CSV":
                    return _marketDataProcessor ?? new MarketDataCsvSourceProcessor(new MarketDataCsvSourceValidator(sourceLocation), new MarketDataCsvParser(sourceLocation));

                    // open for extension for other types of sources

            }
        }
        
        /// <summary>
        /// For UnitTesting purpose only. Injecting the processor into the Factory directly.
        /// </summary>
        /// <param name="processor"></param>
        public static void SetMarketDataSource(IMarketDataSourceProcessor processor)
        {
            _marketDataProcessor = processor;
        }
    }
}
