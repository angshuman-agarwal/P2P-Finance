#region License
// Copyright (c) Angshuman Agarwal, All rights reserved.
// See License.txt in the project root for license information.
#endregion
using System.Globalization;

namespace LoanApp.Angshuman
{
    /// <summary>
    /// Interface for definign the configurable data in the application
    /// </summary>
    public interface IMarketDataConfiguration
    {
        int LoanTenure { get; }
        int RepaymentAmountPrecision { get; }
        int LoanRatePrecision { get; }
        int LoanAmountIncrementor { get; }
        double MinimumLoanAmount { get; }
        double MaximumLoanAmount { get; }
        double BorrowerLoanAmount { get; set; }
        CultureInfo Culture { get; }
    }
}