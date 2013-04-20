#region License
// Copyright (c) Angshuman Agarwal, All rights reserved.
// See License.txt in the project root for license information.
#endregion
using System;
using System.Globalization;
using System.Text;

namespace LoanApp.Angshuman
{
    /// <summary>
    /// Class responsible for processing the loan for the borrower. This class provides the total repayment (monthly/yearly compoiunded) for the borrower
    /// based on the optimal rate from the market pool.
    /// </summary>
    public class LoanProcessor : ILoanProcessor
    {
        private readonly IMarketDataRateCalculator _rateCalculator;
        private readonly double _borrowerAmount;
        private readonly int _tenure;
        private double _monthlyRepayment;
        private double _totalRepayment;
        private readonly IMarketDataConfiguration _marketDataConfig;
        private double _rate;

        /// <summary>
        /// Contructor is injected with the rate calculator and market data config
        /// </summary>
        /// <param name="rateCalculator">Calculates the best rate for the borrower</param>
        /// <param name="marketDataConfiguration">Contains configuration data</param>
        public LoanProcessor(IMarketDataRateCalculator rateCalculator, IMarketDataConfiguration marketDataConfiguration)
        {
            _rateCalculator = rateCalculator;
            _borrowerAmount = marketDataConfiguration.BorrowerLoanAmount;
            _tenure = marketDataConfiguration.LoanTenure;
            _marketDataConfig = marketDataConfiguration;
        }

        /// <summary>
        /// Function responsible for doing the repayment calculation based on the rate
        /// </summary>
        public void ProcessLoan()
        {
            _rate = _rateCalculator.CalculateBestRate(_borrowerAmount);
            
            // Monthly rate = (1 + annual rate)^(1/12) – 1
            const double period = (double) 1/12;
            var effectiveMonthlyRate = Math.Pow((1 + _rate), period) - 1;

            // annuity factor =  (1-(1+r)^-n)/r
            var annuityFactor = (1 - Math.Pow((1 + effectiveMonthlyRate), -(_tenure))) / effectiveMonthlyRate;

            _monthlyRepayment = _borrowerAmount / annuityFactor;
            _totalRepayment = _monthlyRepayment * _tenure;
        }

        /// <summary>
        /// For UnitTesting purpose. 
        /// </summary>
        public double MonthlyRepayment
        {
            get
            {
                return Math.Round(_monthlyRepayment,2);
            }
        }

        /// <summary>
        /// For UnitTesting purpose. 
        /// </summary>
        public double TotalRepayment
        {
            get
            {
                return Math.Round(_totalRepayment, 2);
            }
        }

        /// <summary>
        /// Requested amount: £XXXX
        /// Rate: X.X%
        /// Monthly repayment: £XXXX.XX
        /// Total repayment: £XXXX.XX
        /// </summary>
        /// <returns>Formatted output</returns>
        public override string ToString()
        {
            var formatInfo = GetNumberFormat();
            var sb = new StringBuilder();
            
            // C0 denotes there will not be any decimal places in the currency
            sb.Append(string.Format("Requested amount: {0}", _marketDataConfig.BorrowerLoanAmount.ToString("C0", formatInfo)));
            sb.Append(Environment.NewLine);
            sb.Append(string.Format(GetRateFormat(), _rate * 100)); //"Rate: {0:F1}%"
            sb.Append(Environment.NewLine);
            sb.Append(string.Format("Monthly repayment: {0}", _monthlyRepayment.ToString(GetRepaymentPrecision(), formatInfo)));
            sb.Append(Environment.NewLine);
            sb.Append(string.Format("Total repayment: {0}", _totalRepayment.ToString(GetRepaymentPrecision(), formatInfo)));

            return sb.ToString();
        }

        /// <summary>
        /// Helper function to get rid of the comma separator from the number
        /// </summary>
        /// <returns></returns>
        private NumberFormatInfo GetNumberFormat()
        {
            var formatInfo = (NumberFormatInfo)_marketDataConfig.Culture.NumberFormat.Clone();
            formatInfo.CurrencyGroupSeparator = string.Empty;
            return formatInfo;
        }

        /// <summary>
        /// Read the config file to set the rate decimal preciosion using the Fixed-point precision specifier
        /// </summary>
        /// <returns></returns>
        private string GetRateFormat()
        {
            const string format = "Rate: {{0:F{0}}}%";
            return string.Format(format, _marketDataConfig.LoanRatePrecision);
        }

        private string GetRepaymentPrecision()
        {
            return "C" + _marketDataConfig.RepaymentAmountPrecision;
        }
    }
}
