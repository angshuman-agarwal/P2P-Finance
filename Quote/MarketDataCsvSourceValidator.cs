#region License
// Copyright (c) Angshuman Agarwal, All rights reserved.
// See License.txt in the project root for license information.
#endregion
using System.IO;

namespace LoanApp.Angshuman
{
    /// <summary> 
    /// Class for validating the market data processor. In this case it is a csv processor file validator
    /// </summary>
    public class MarketDataCsvSourceValidator : IMarketDataSourceValidator
    {
        public MarketDataCsvSourceValidator(string source)
        {
            Source = source;
        }
        public bool MarketDataSourceExists()
        {
            return File.Exists(Source);
        }

        public bool MarketDataSourceIsValid()
        {
            return Path.GetExtension(Source) == ".csv";
        }

        public bool MarketDataSourceIsEmpty()
        {
            return (new FileInfo(Source).Length == 0);
        }
         
        public string Source { get; private set; }
    }
}