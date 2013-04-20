#region License
// Copyright (c) Angshuman Agarwal, All rights reserved.
// See License.txt in the project root for license information.
#endregion
using System;
using System.Collections.Generic;
using System.IO;

namespace LoanApp.Angshuman
{
    /// <summary>
    /// 
    /// </summary>
    public class MarketDataCsvSourceProcessor : IMarketDataSourceProcessor
    {
        private readonly IMarketDataParser _parser;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="validator"></param>
        /// <param name="parser"></param>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="FileNotFoundException"></exception>
        /// <exception cref="IOException"></exception>
        /// <exception cref="Exception"></exception>
        public MarketDataCsvSourceProcessor(IMarketDataSourceValidator validator, IMarketDataParser parser)
        {
            if (parser == null)
            {
                throw new ArgumentNullException("parser");
            }

            if (validator == null)
            {
                throw new ArgumentNullException("validator");
            }

            if (!validator.MarketDataSourceExists())
            {
                throw new FileNotFoundException(string.Format("Market data processor file not found."));
            }

            if (!validator.MarketDataSourceIsValid())
            {
                throw new IOException(string.Format("Market data file supplied is not a valid csv file."));
            }

            if(validator.MarketDataSourceIsEmpty())
            {
                // TODO: Replace with custom exception
                throw new Exception(string.Format("Market data file is empty."));
            }

            _parser = parser;

        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public IList<IMarketData> GetMarketData()
        {
            return _parser.Parse();
        }
    }
}
