#region License
// Copyright (c) Angshuman Agarwal, All rights reserved.
// See License.txt in the project root for license information.
#endregion
using System;
using System.Text;

namespace LoanApp.Angshuman
{
    class Program
    {
        static void Main(string[] args)
        {
            // basic checks for command line parameters
            if (args.Length < 2)
            {
                PrintUsage();
                Console.ReadLine();
                return;
            }

            string marketDataSource = args[0];
            double borrowerAmount;
            double.TryParse(args[1], out borrowerAmount);

            // main program
            try
            {
                // read the config data
                var marketDataConfig = new MarketDataConfiguration();
                
                // get the appropriate source (market.csv in this case)
                var marketSource = MarketDataSourceFactory.GetMarketDataSource(marketDataSource);
                // populate the market data from the source
                var marketData = marketSource.GetMarketData();

                // validate the market data with rules as per the requirement
                var validator = new LoanValidator(marketDataConfig, marketData, borrowerAmount);
                string message;
                
                // notify the user with appropriate error messages on unsuccessful validation
                if (!validator.ValidateLoan(out message))
                {
                    Console.WriteLine(message);
                    return;
                }

                // do the loan calculation for motnhtly & total repayment based on best market rate.
                var loanProcessor = new LoanProcessor(new MarketDataRateCalculator(marketData), marketDataConfig);
                loanProcessor.ProcessLoan();

                // prints the output as per the requirement
                Console.WriteLine(loanProcessor.ToString());
            }
            catch (Exception ex) // just catching the exception and printing the message
            {
               Console.WriteLine(ex.Message);
            }
            Console.ReadLine();
        }

        /// <summary>
        /// Helper function for printing the usage of the exe.
        /// </summary>
        private static void PrintUsage()
        {
            var sb = new StringBuilder();
            sb.Append("Usage:\r\n");
            sb.Append("\t\t[application] [market_file] [loan_amount]");
            sb.Append("\r\nExample:\r\n");
            sb.Append("\t\tquote.exe market.csv 1000");
            Console.WriteLine(sb.ToString());
        }
    }
}
