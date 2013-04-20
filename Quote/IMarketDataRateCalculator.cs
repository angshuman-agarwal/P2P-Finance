#region License
// Copyright (c) Angshuman Agarwal, All rights reserved.
// See License.txt in the project root for license information.
#endregion
namespace LoanApp.Angshuman
{
    /// <summary>
    /// Interface defining the rate calculation
    /// </summary>
    public interface IMarketDataRateCalculator
    {
        /// <summary>
        /// Method for defining the algorithm of best rate
        /// </summary>
        /// <param name="borrowerAmount"></param>
        /// <returns></returns>
        double CalculateBestRate(double borrowerAmount);
    }
}