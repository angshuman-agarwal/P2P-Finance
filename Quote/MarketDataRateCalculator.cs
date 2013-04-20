#region License
// Copyright (c) Angshuman Agarwal, All rights reserved.
// See License.txt in the project root for license information.
#endregion
using System;
using System.Collections.Generic;
using System.Linq;

namespace LoanApp.Angshuman
{
    /// <summary>
    /// Class for calculating the optimal rate for the borrower based on the market data's lending rates
    /// </summary>
    public class MarketDataRateCalculator : IMarketDataRateCalculator
    {
        private readonly IList<IMarketData> _marketData;

        /// <summary>
        /// Contructor
        /// </summary>
        /// <param name="marketData">Market data list</param>
        public MarketDataRateCalculator(IList<IMarketData> marketData)
        {
            if (marketData == null)
            {
                throw new ArgumentNullException("marketData");
            }

            _marketData = marketData;
        }

        /// <summary>
        /// Method to calculate the best rate for the borrower's amount based on the lenders' rate off the market data
        /// </summary>
        /// <returns></returns>
        public double CalculateBestRate(double borrowerAmount)
        {
            if (borrowerAmount <= 0)
            {
                throw new Exception("Requested amount should be more than 0");
            }

            if (borrowerAmount > _marketData.Sum(m => m.Lender.ActualAmount))
            {
                throw new Exception("Requested amount is more than the total market pool money.");
            }

            // sort the lenders' rate in ascending order
            var marketData = from m in _marketData orderby m.Lender.Rate ascending select m;


            double temp=0;
            double loanWeightFactor = 0;
            foreach (var data in marketData)
            {
                // keep track of how much more money is required to satisfy the borrower loan amount
                var remaining = (borrowerAmount - temp);

                // track the difference between borrower and lender amounts
                var diffBetweenBorrowerAndLender = (remaining - data.Lender.ActualAmount);
                
                // borrower needs more money, utilised full money of lender
                if (diffBetweenBorrowerAndLender > 0)
                {
                    temp += data.Lender.ActualAmount;
                    data.Lender.RemainingAmount = 0;
                    loanWeightFactor += data.Lender.Rate*data.Lender.ActualAmount;
                }
                else // Lender is still left with money after giving to borrower. 
                {
                    temp += remaining;
                    // update the lender's remaining amount
                    data.Lender.RemainingAmount = Math.Abs(diffBetweenBorrowerAndLender);
                    // caclulate the loan factor on the utilised amount only
                    loanWeightFactor += data.Lender.Rate*remaining;
                }

                // borrower's amount is satisfied. break out
                if (borrowerAmount == temp)
                {
                    break;
                }
            }
            
            // calculate the final weighted rate
            return loanWeightFactor / borrowerAmount;
        }
    }
}
