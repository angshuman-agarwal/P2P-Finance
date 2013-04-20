#region License
// Copyright (c) Angshuman Agarwal, All rights reserved.
// See License.txt in the project root for license information.
#endregion
namespace LoanApp.Angshuman
{
    /// <summary>
    /// Class defining the lenders' market data
    /// </summary>
    public class MarketData : IMarketData
    {
        /// <summary>
        /// The lender object which contains the lender specific details
        /// </summary>
        public ILender Lender { get; set; }
    }
}
