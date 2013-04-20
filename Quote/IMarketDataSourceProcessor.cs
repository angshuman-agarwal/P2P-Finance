#region License
// Copyright (c) Angshuman Agarwal, All rights reserved.
// See License.txt in the project root for license information.
#endregion
using System.Collections.Generic;

namespace LoanApp.Angshuman
{
    /// <summary>
    /// This interface gives the parsed market data from a source liek csv file or a web service or a database, etc..
    /// </summary>
    public interface IMarketDataSourceProcessor
    {
        /// <summary>
        /// Method which returns the list of parsed Market Data
        /// </summary>
        /// <returns></returns>
        IList<IMarketData> GetMarketData();
    }
}
