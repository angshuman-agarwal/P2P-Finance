#region License
// Copyright (c) Angshuman Agarwal, All rights reserved.
// See License.txt in the project root for license information.
#endregion
using System.Collections.Generic;

namespace LoanApp.Angshuman
{
    /// <summary>
    /// Generic parser interface for the market data
    /// </summary>
    public interface IMarketDataParser
    {
        /// <summary>
        /// Function to parse the market data which returns a collection of Market Data
        /// </summary>
        /// <returns></returns>
        IList<IMarketData> Parse();
    }
}