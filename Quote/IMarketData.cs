#region License
// Copyright (c) Angshuman Agarwal, All rights reserved.
// See License.txt in the project root for license information.
#endregion
namespace LoanApp.Angshuman
{
    /// <summary>
    /// Interface defining the market data with lender spcific details
    /// </summary>
    public interface IMarketData
    {
        ILender Lender { get; set; }
    }
}