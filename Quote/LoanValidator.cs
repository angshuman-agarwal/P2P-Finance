#region License
// Copyright (c) Angshuman Agarwal, All rights reserved.
// See License.txt in the project root for license information.
#endregion
using System.Collections.Generic;
using System.Linq;

namespace LoanApp.Angshuman
{
    /// <summary>
    /// Class responsible for doing basic validations against the parameters supplied by the user for loan calculation
    /// </summary>
    public class LoanValidator : ILoanValidator
    {
        private readonly IMarketDataConfiguration _configuration;
        private readonly IList<IMarketData> _marketData;

        public LoanValidator(IMarketDataConfiguration configuration, IList<IMarketData> marketData, double loanAmount)
        {
            _marketData = marketData;
            _configuration = configuration;
            configuration.BorrowerLoanAmount = loanAmount;
        }

        public bool ValidateLoan(out string message)
        {
            message = string.Empty;

            // Loan amount should be in £100 increment between min. and max. inclusive

            if ((_configuration.BorrowerLoanAmount % _configuration.LoanAmountIncrementor) != 0)
            {
                message = string.Format("Loan should be multiple of {0} and between {1} - {2} inclusive", 
                    _configuration.LoanAmountIncrementor.ToString("C", _configuration.Culture),
                    _configuration.MinimumLoanAmount.ToString("C", _configuration.Culture),
                    _configuration.MaximumLoanAmount.ToString("C", _configuration.Culture)
                    );

                return false;
            }
            
            if (_configuration.BorrowerLoanAmount < _configuration.MinimumLoanAmount)
            {
                message = string.Format("Minimum loan amount should be at least {0}", _configuration.MinimumLoanAmount.ToString("C", _configuration.Culture));
                return false;
            }

            if (_configuration.BorrowerLoanAmount > _configuration.MaximumLoanAmount)
            {
                message = string.Format("Total loan amount cannot exceed {0}", _configuration.MaximumLoanAmount.ToString("C", _configuration.Culture));
                return false;
            }

            var totalPoolMoney = _marketData.Sum(m => m.Lender.ActualAmount);
            if (_configuration.BorrowerLoanAmount > totalPoolMoney)
            {
                message = "Sorry, we are not able to provide a quote at this moment.";
                return false;
            }

            return true;
        }
    }
}