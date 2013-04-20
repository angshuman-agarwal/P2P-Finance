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
    /// Class for parsing the csv file 
    /// </summary>
    public class MarketDataCsvParser : IMarketDataParser
    {
        private readonly string _source;

        /// <summary>
        /// Constructor takes the source location fo the csv file
        /// </summary>
        /// <param name="sourceLocation">CSV File Path</param>
        public MarketDataCsvParser(string sourceLocation)
        {
            _source = sourceLocation;
        }

        /// <summary>
        /// The method parses the csv file using a very basic parsing mechanism and returns the market data collection
        /// </summary>
        /// <returns>Collection of market data</returns>
        /// <exception cref="Exception"></exception>
        public IList<IMarketData> Parse()
        {
            // TODO: delimiter can be pushed to config file
            const char delimiter = ',';
            IList<IMarketData> marketDataList = new List<IMarketData>();
            using (var sr = new StreamReader(_source))
            {
                // read the header before getting into the loop
                // Assuming that the csv file will always have a header
                sr.ReadLine();
                string line;
                int lineNumber = 1;
                while ((line = sr.ReadLine()) != null)
                {
                    lineNumber++;
                    var fields = line.Split(delimiter);

                    if (string.IsNullOrEmpty(line))
                    {
                        throw new Exception(string.Format("{0} has empty lines. Please see line number {1} and fix the data", _source, lineNumber));
                    }

                    if (fields.Length < 3)
                    {
                        throw new Exception(string.Format("{0} has incomplete/invalid data. Please see line number {1} and fix the data", _source, lineNumber));
                    }

                    // NOTE: There are more validations which can be done over file data
                    // 1. The fields are shuffled
                    // 2. One of the field is an empty string
                    // 3. Validating the integrity of the data - For ex: rate is negative, amount is not a number, etc...

                    var marketData = new MarketData
                    {
                        Lender = new Lender()
                        {
                            // name of the lender
                            Name = fields[0]
                        }
                    };
                    double rate;
                    // rate of the Lender
                    double.TryParse(fields[1], out rate);
                    marketData.Lender.Rate = rate;

                    double amount;
                    // amount of the Lender
                    double.TryParse(fields[2], out amount);
                    marketData.Lender.ActualAmount = amount;

                    marketDataList.Add(marketData);
                }
            }
            return marketDataList;
        }
    }
}
